//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ExportImportExcel.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class ImageInfo
    {
        public int id { get; set; }
        public Nullable<int> image_id { get; set; }
        public string image_link { get; set; }
        public string predict_label { get; set; }
        public Nullable<int> actual_label_id { get; set; }
        public Nullable<System.DateTime> created_at { get; set; }
        public Nullable<System.DateTime> updated_at { get; set; }
        public Nullable<System.DateTime> deleted_at { get; set; }
    }
}
