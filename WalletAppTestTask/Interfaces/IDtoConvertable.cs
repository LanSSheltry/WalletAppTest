namespace WalletAppTestTask.Interfaces
{
    public interface IDtoConvertable<TDto>
    {
        public TDto ToDto();
    }
}
