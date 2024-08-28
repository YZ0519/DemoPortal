using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimplePOSWeb.ViewModel.OptionSet;

namespace SimplePOSWeb.Helper
{
    public class ConversionHelper
    {
        public static List<SelectListItem> ConvertToSelectList(List<OptionSetVM> list)
        {
            List<SelectListItem> selectList = new List<SelectListItem>();
            if (list.Count == 0)
            {
                return selectList;
            }
            foreach (var x in list)
            {
                SelectListItem item = new SelectListItem();
                item.Text = x.Text;
                item.Value = x.Value;
                selectList.Add(item);
            }
            return selectList;
        }
    }
}
