﻿using System;
using System.Collections.Generic;

#nullable disable

namespace AE.Models
{
    public partial class Ship
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double Velocity { get; set; }
    }
}
