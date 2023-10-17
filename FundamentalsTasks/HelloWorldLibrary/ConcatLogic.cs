namespace HelloWorldLibrary
{
    public class ConcatLogic
    {
        public ConcatLogic()
        {
            CurrentTime = DateTime.Now;
        }

        public DateTime CurrentTime { get; set; }

        public string Concat(string username)
        {
            var result = $"{CurrentTime} Hello, {username}!";
            
            return result;
        }

        public string ConcatTypeTwo(string username)
        {
            var result = CurrentTime + " Hello, " + username + "!";
           
            return result;
        }
    }
}