namespace Utils
{
    public class IdGen
    {
        private static int id = 0;

        public int NewId()
        {
            return id++;
        }        
    }
}
