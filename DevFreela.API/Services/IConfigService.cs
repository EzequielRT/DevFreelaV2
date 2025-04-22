namespace DevFreela.API.Services
{
    public interface IConfigService
    {
        int GetValue();
    }

    partial class ConfigService : IConfigService
    {
        private int _value;
        public int GetValue()
        {
            _value++;
            return _value;
        }
    }
}
