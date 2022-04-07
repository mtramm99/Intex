using Microsoft.ML.OnnxRuntime.Tensors;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Intex.Models
{
    public class collisionPredictor
    {
        public float crash_datetime { get; set; }
        public float milepoint { get; set; }
        public float intersection_related { get; set; }
        public float teenage_driver_involved { get; set; }
        public float night_dark_condition { get; set; }
        public float single_vehicle { get; set; }
        public float county_name_SALT_LAKE { get; set; }
        // the SALT_LAKE was spelled SALT LAKE

        public Tensor<float> AsTensor()
        {
            float[] data = new float[]
            {
                crash_datetime, milepoint, intersection_related,
                teenage_driver_involved, night_dark_condition,
                single_vehicle, county_name_SALT_LAKE
            };
            int[] dimensions = new int[] { 1, 7 };
            return new DenseTensor<float>(data, dimensions);
        }
    }
}
