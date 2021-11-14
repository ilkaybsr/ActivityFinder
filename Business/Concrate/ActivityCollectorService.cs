using Business.Abstracts;
using Business.DTO;
using DataAccess.Abstracts;
using DataAccess.Entities;
using HtmlAgilityPack;
using RestSharp;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Business.Concrate
{
    public class ActivityCollectorService : IActivityCollectorService
    {
        private readonly IGenericRepository<Activity> _activityRepository;
        public ActivityCollectorService(IGenericRepository<Activity> activityRepository)
        {
            _activityRepository = activityRepository;
        }

        public async Task<List<ActivityDTO>> Collect(int limit)
        {
            var client = new RestClient("https://biletinial.com/tiyatro");
            client.Timeout = 60000;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Connection", "keep-alive");
            request.AddHeader("Cache-Control", "max-age=0");
            request.AddHeader("sec-ch-ua", "\"Google Chrome\";v=\"95\", \"Chromium\";v=\"95\", \";Not A Brand\";v=\"99\"");
            request.AddHeader("sec-ch-ua-mobile", "?0");
            request.AddHeader("sec-ch-ua-platform", "\"Windows\"");
            request.AddHeader("Upgrade-Insecure-Requests", "1");
            client.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/95.0.4638.54 Safari/537.36";
            request.AddHeader("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9");
            request.AddHeader("Sec-Fetch-Site", "same-origin");
            request.AddHeader("Sec-Fetch-Mode", "navigate");
            request.AddHeader("Sec-Fetch-User", "?1");
            request.AddHeader("Sec-Fetch-Dest", "document");
            request.AddHeader("Referer", "https://biletinial.com/");
            request.AddHeader("Accept-Language", "tr-TR,tr;q=0.9,en-US;q=0.8,en;q=0.7");
            IRestResponse response = client.Execute(request);

            List<ActivityDTO> activites = new List<ActivityDTO>();

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var doc = new HtmlDocument();
                doc.LoadHtml(response.Content);

                var aElements = doc.DocumentNode.SelectNodes("//a[@class='flex directionColumn justifyStart alignStart']");

                for (int i = 0; i < limit; i++)
                {
                    var activityDetails = getActivityDetails("https://biletinial.com" + aElements[i].Attributes["href"]?.Value);
                    activites.Add(activityDetails);
                }

                List<Activity> willBeAddForDB = new();
                activites.ForEach(x => willBeAddForDB.Add(Activity.Create(x.Name, x.Description, x.Address, x.Category, x.Location, x.Date)));

                await _activityRepository.AddRangeAsync(willBeAddForDB);
                await _activityRepository.SaveChangesAsync();

                return willBeAddForDB.Select(x => new ActivityDTO
                {
                    Id = x.Id,
                    Address = x.Address,
                    Category = x.Category,
                    Date = x.Date,
                    Description = x.Description,
                    Location = x.Location,
                    Name = x.Name
                }).ToList();
            }

            return null;
        }

        public async Task<List<ActivityDTO>> GetAllActivities(int pageIndex, int itemSize)
        {
            //todo tablodan veri çektikten sonra pagination yaptık, bu doğru bir yaklaşım değil (düzenle)
            var all = (await _activityRepository.GetAllAsync()).Select(x => new ActivityDTO
            {
                Name = x.Name,
                Date = x.Date,
                Address = x.Address,
                Location = x.Location,
                Category = x.Category,
                Description = x.Description,
            })
                .Skip(pageIndex * itemSize)
                .Take(itemSize)
                .ToList();

            return all;
        }

        private ActivityDTO getActivityDetails(string href)
        {
            var client = new RestClient(href);
            client.Timeout = 60000;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Connection", "keep-alive");
            request.AddHeader("Cache-Control", "max-age=0");
            request.AddHeader("sec-ch-ua", "\"Google Chrome\";v=\"95\", \"Chromium\";v=\"95\", \";Not A Brand\";v=\"99\"");
            request.AddHeader("sec-ch-ua-mobile", "?0");
            request.AddHeader("sec-ch-ua-platform", "\"Windows\"");
            request.AddHeader("Upgrade-Insecure-Requests", "1");
            client.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/95.0.4638.54 Safari/537.36";
            request.AddHeader("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9");
            request.AddHeader("Sec-Fetch-Site", "same-origin");
            request.AddHeader("Sec-Fetch-Mode", "navigate");
            request.AddHeader("Sec-Fetch-User", "?1");
            request.AddHeader("Sec-Fetch-Dest", "document");
            request.AddHeader("Referer", "https://biletinial.com/tiyatro?date=2021-11-01&thisweekend=false&filmTypeId=");
            request.AddHeader("Accept-Language", "tr-TR,tr;q=0.9,en-US;q=0.8,en;q=0.7");
            IRestResponse response = client.Execute(request);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var doc = new HtmlDocument();
                doc.LoadHtml(response.Content);

                string name = doc.DocumentNode.SelectSingleNode("//h1[@itemprop='name']")?.InnerText;
                string location = doc.DocumentNode.SelectSingleNode("//span[@class='eventLoc']")?.InnerText;
                string date = doc.DocumentNode.SelectSingleNode("//span[@class='seanceDate']")?.InnerText;
                string address = doc.DocumentNode.SelectSingleNode("//span[@class='eventAddress']")?.InnerText;
                string description = doc.DocumentNode.SelectSingleNode("//div[@data-content='event-desc']")?.InnerText;
                string category = doc.DocumentNode.SelectSingleNode("//span[@itemprop='genre']")?.InnerText;

                var result = new ActivityDTO
                {
                    Category = clearHtml(category),
                    Name = clearHtml(name),
                    Location = clearHtml(location),
                    Address = clearHtml(address),
                    Description = clearHtml(description),
                    Date = clearHtml(date)
                };

                return result;
            }

            //todo 200 dışında cevap gelir ise retry mantığı kurgulamalıyız.
            return null;
        }

        string clearHtml(string referansHtml)
        {
            var res = referansHtml.Replace("\r", "")?.Replace("\n", "")?.Replace("\t", "")
                    ?.Replace("  ", "");

            return HttpUtility.HtmlDecode(res);
        }
    }
}
