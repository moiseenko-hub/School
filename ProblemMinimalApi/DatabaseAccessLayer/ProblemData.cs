﻿using ProblemMinimalApi.Tests;

namespace ProblemMinimalApi.DatabaseAccessLayer
{
    public class ProblemData
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Theme { get; set; }
        
        public string TestName { get; set; }
    }
}
