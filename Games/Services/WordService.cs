namespace Games.Services
{
    public interface IWordService
    {
        Task<string> Get();
        Task<string[]> GetWords();
    }

    public class WordService : IWordService
    {
        private HttpClient httpClient;
        public WordService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        public async Task<string> Get()
        {
            string text;
            try
            {
                text = await httpClient.GetStringAsync($"Wordles.txt");
            }
            catch
            {
                text = $"cigar\nrebut\nsissy\nhumph\nawake\nblush\nfocal\nevade\nnaval\nserve\n" +
                "heath\ndwarf\nmodel\nkarma\nstink\ngrade\nquiet\nbench\nabate\nfeign\nmajor\n" +
                "death\nfresh\ncrust\nstool\ncolon\nabase\nmarry\nreact\nbatty\npride\nfloss\n" +
                "helix\ncroak\nstaff\npaper\nunfed\nwhelp\ntrawl\noutdo\nadobe\ncrazy\nsower\n" +
                "repay\ndigit\ncrate\ncluck\nspike\nmimic\npound";
            }

            return text;
        }

        public async Task<string[]> GetWords()
        {
            string[] words;

            string text = await Get();

            words = text.Split(new string[] { "\n", "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

            return words;
        }
    }
}
