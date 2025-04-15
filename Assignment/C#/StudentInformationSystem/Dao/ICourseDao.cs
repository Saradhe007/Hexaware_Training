using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentInformationSystem.Entity;

namespace StudentInformationSystem.Dao
{
    
        public interface ICourseDao
        {
            Course? GetCourseByCode(string courseCode);
        }
    
}
