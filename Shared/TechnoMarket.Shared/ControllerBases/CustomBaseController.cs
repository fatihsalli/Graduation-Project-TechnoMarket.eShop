using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnoMarket.Shared.Dtos;

namespace TechnoMarket.Shared.ControllerBases
{
    public class CustomBaseController:ControllerBase
    {
        public IActionResult CreateActionResult<T>(CustomResponseDto<T> response)
        {
            if (response.StatusCode==204)
            {
                return new ObjectResult(null) 
                { 
                    StatusCode = response.StatusCode 
                };
            }

            return new ObjectResult(response)
            {
                StatusCode=response.StatusCode,
            };
        }




    }
}
