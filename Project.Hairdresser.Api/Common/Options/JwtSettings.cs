namespace Common.Options
{
    public class JwtSettings
    {
        public string Secret { get; set; }
        public TimeSpan TplemLifeTime { get; set; }
    }
}
