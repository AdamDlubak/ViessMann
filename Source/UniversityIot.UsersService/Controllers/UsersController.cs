namespace UniversityIot.UsersService.Controllers
{
    using System.Threading.Tasks;
    using System.Web.Http;
    using UsersDataAccess.Models;
    using UsersDataService;
    using Models;

    [RoutePrefix("users")]
    public class UsersController : ApiController
    {
        private readonly IUsersDataService usersDataService;

        public UsersController(IUsersDataService usersDataService)
        {
            this.usersDataService = usersDataService;
        }

        //---------- Notatka
        // return Ok("Komunikat do zwrócenia"); --- HTTP 200
        // return NotFound(); --- HTTP 404
        // return BadRequest();
        // Błąd 400 np. błąd walidacji i wtedy z błędami walidacji należy zwrócić
        // PUT do edycji, numer id i model w parametrach

        [Route("")]
        public async Task<IHttpActionResult> Get()
        {
            return BadRequest();
        }

        [Route("{id:int}")]
        public async Task<IHttpActionResult> Get(int id)
        {
            var user = await usersDataService.GetUserAsync(id);
            if (user != null)
            {
                var userToReturn = MapUser(user);
                return Ok(userToReturn);
            }
            return NotFound();
        }

        [Route("")]
        public async Task<IHttpActionResult> Post(AddUserViewModel userVM)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var addedUser = await usersDataService.AddUserAsync(new User()
            {
                CustomerNumber = userVM.CustomerNumber,
                Name = userVM.Name,
                Password = userVM.Password
            });
            var addedUserVM = MapUser(addedUser);
            return Ok(addedUserVM);
        }


        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> Delete(int id)
        {
            if (await usersDataService.GetUserAsync(id) != null)
            {
                await usersDataService.DeleteUserAsync(id);
                return Ok("Użytkownik usunięty pomyślnie!");
            }
            return NotFound();
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> Put(int id, EditUserViewModel editUserViewModel)
        {
            var user = await usersDataService.GetUserAsync(id);
            if (user != null)
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);

                user.CustomerNumber = editUserViewModel.CustomerNumber;
                await usersDataService.UpdateUserAsync(user);
                var UserToReturn = MapUser(user);

                return Ok(UserToReturn);
            }
            return NotFound();
        }

        private static UserViewModel MapUser(User user)
        {
            var userVM = new UserViewModel()
            {
                CustomerNumber = user.CustomerNumber,
                Id = user.Id,
                Name = user.Name,
                Password = user.Password
            };

            foreach (var userGateway in user.UserGateways)
            {
                userVM.UserGateways.Add(new UserGatewayViewModel()
                {
                    GatewaySerial = userGateway.GatewaySerial,
                    Id = userGateway.Id,
                    AccessType = userGateway.AccessType.ToString()
                });
            }
            return userVM;
        }
    }
}