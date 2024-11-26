using System.Linq.Expressions;

namespace ApiCatalogo.Repositories
{
    public interface IRepository<T>
    {
        IEnumerable<T> GetAll();
        T? Get(Expression<Func<T,bool>>predicate); //Expression = Traduz a lógica que será determinada no predicate para que o framework compreenda e faça o trabalho adequado no banco de dados. Func = Define a assinatura da lógica determinando quem é a entrada e qual a saída de dados. Predicate = Determina a lógica para o filtro que o expression precisa.
        T Create(T entity);
        T Update(T entity);
        T Delete(T entity);
    }


}
