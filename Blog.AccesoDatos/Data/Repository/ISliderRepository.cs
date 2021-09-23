﻿using Blog.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.AccesoDatos.Data.Repository
{
    public interface ISliderRepository : IRepository<Slider>
    {
        IEnumerable<SelectListItem> GetListSliders();

        void update(Slider slider);
    }
}
