using Microsoft.ML.OnnxRuntime.Tensors;
using System;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Intex.Models
{
    public class Collision
    {
        [Key]
        [Required]
        public int CRASH_ID { get; set; }

        // [Required (ErrorMessage="Please Enter the Date & Time")]
        public DateTime CRASH_DATETIME { get; set; }

        [Required (ErrorMessage = "Please Enter a Route Number")]
        public string ROUTE { get; set; }

        [Required (ErrorMessage = "Please enter a Milepoint")]
        public float MILEPOINT { get; set; }
        public float LAT_UTM_Y { get; set; }
        public float LONG_UTM_X { get; set; }

        [Required (ErrorMessage = "Please enter a Main Road Name")]
        public string MAIN_ROAD_NAME { get; set; }

        public string CITY { get; set; }

        [Required (ErrorMessage = "Please enter a County Name")]
        public string COUNTY_NAME { get; set; }

        [Required (ErrorMessage = "Please enter the Crash Severity")]
        public float CRASH_SEVERITY_ID { get; set; }
        public float WORK_ZONE_RELATED { get; set; }
        public float PEDESTRIAN_INVOLVED { get; set; }
        public float BICYCLIST_INVOLVED { get; set; }
        public float MOTORCYCLE_INVOLVED { get; set; }
        public float IMPROPER_RESTRAINT { get; set; }
        public float UNRESTRAINED { get; set; }
        public float DUI { get; set; }
        public float INTERSECTION_RELATED { get; set; }
        public float WILD_ANIMAL_RELATED { get; set; }
        public float DOMESTIC_ANIMAL_RELATED { get; set; }
        public float OVERTURN_ROLLOVER { get; set; }
        public float COMMERCIAL_MOTOR_VEH_INVOLVED { get; set; }
        public float TEENAGE_DRIVER_INVOLVED { get; set; }
        public float OLDER_DRIVER_INVOLVED { get; set; }
        public float NIGHT_DARK_CONDITION { get; set; }
        public float SINGLE_VEHICLE { get; set; }
        public float DISTRACTED_DRIVING { get; set; }
        public float DROWSY_DRIVING { get; set; }
        public float ROADWAY_DEPARTURE { get; set; }

       
    }
}
