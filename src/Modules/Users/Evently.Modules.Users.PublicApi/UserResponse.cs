using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evently.Modules.Users.PublicApi;

/// <summary>
/// Ответ на запрос пользователя
/// </summary>
/// <param name="Id"> Идентификатор пользователя </param>
/// <param name="Email"> Email пользователя </param>
/// <param name="FirstName"> Имя пользователя </param>
/// <param name="LastName"> Фамилия пользователя </param>
public sealed record UserResponse(
    Guid Id,
    string Email,
    string FirstName,
    string LastName);
