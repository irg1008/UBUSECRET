namespace Utils
{
    public class IdGen
    {
        private static int userId = 0;
        private static int secretId = 0;

        public static int NewUserId()
        {
            return userId++;
        }        
        
        public static int NewSecretId()
        {
            return secretId++;
        }
    }
}
