namespace Team3.SavingLoading.SaveData
{
    public interface ISaveData
    {
        public bool TryRead();
        public void Write();
    }
}
