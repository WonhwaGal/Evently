using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evently.Modules.Ticketing.Domain.Customers;

public interface ICustomerRepository
{

    /// <summary>
    /// Получить покупателя по идентификатору
    /// </summary>
    /// <param name="id"> Идентификатор покупателя </param>
    /// <param name="cancellationToken"> Токен отмены </param>
    /// <returns></returns>
    Task<Customer?> GetAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Добавить покупателя
    /// </summary>
    /// <param name="customer"> Покупатель </param>
    void Insert(Customer customer);

}
