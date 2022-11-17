namespace PayCalculator.Data
{
    public interface IEmployeeRepo<T>
    {
        IEnumerable<T> GetAll();
        T GetEmployee(int id);
        T Create(T Employee);
        bool Delete(int id);
        T Update(T Employee);
    }
}
