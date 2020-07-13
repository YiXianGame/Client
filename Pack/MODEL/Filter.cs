using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
namespace Make.MODEL
{
    static public class Filter
    {
        public static IEnumerable<SkillCardsModel> SkillCardsModel(List<SkillCardsModel> query, int value,SkillCard BluePrint)
        {
            IEnumerable<SkillCardsModel> result = from SkillCardsModel item in query select item;
            if (BluePrint.Is_Magic)
            {
                result = (from SkillCardsModel item in result
                          where item.SkillCards[value].Is_Magic == true
                          select item);
            }
            if (BluePrint.Is_Physics && result!=null)
            {
                result = (from SkillCardsModel item in result
                          where item.SkillCards[value].Is_Physics == true
                          select item);
            }
            if (BluePrint.Is_Cure && result != null)
            {
                result = (from SkillCardsModel item in result
                          where item.SkillCards[value].Is_Cure == true
                          select item);
            }
            if (BluePrint.Is_Eternal && result != null)
            {
                result = (from SkillCardsModel item in result
                          where item.SkillCards[value].Is_Eternal == true
                          select item);
            }
            if (BluePrint.Is_Attack && result != null)
            {
                result = (from SkillCardsModel item in result
                          where item.SkillCards[value].Is_Attack == true
                          select item);
            }
            if (BluePrint.Name!="" && result != null)
            {
                result = (from SkillCardsModel item in result
                          where item.SkillCards[value].Name.Contains(BluePrint.Name)
                          select item);
            }
            if (BluePrint.State != -1 && result != null)
            {
                result = (from SkillCardsModel item in result
                          where item.SkillCards[value].State==BluePrint.State
                          select item);
            }
            return result;
        }
        public static IEnumerable<Adventure> Adventure(List<Adventure> query, Adventure BluePrint)
        {
            IEnumerable<Adventure> result = from Adventure item in query select item;
            if (BluePrint.Name != "" && result != null)
            {
                result = (from Adventure item in result
                          where item.Name.Contains(BluePrint.Name)
                          select item);
            }
            if (BluePrint.State != -1 && result != null)
            {
                result = (from Adventure item in result
                          where item.State == BluePrint.State
                          select item);
            }
            return result;
        }
        public static IEnumerable<SkillCardsModel> Basic_SkillCardsModel(List<SkillCardsModel> query)
        {
            IEnumerable<SkillCardsModel> result = from SkillCardsModel item in query select item;
                result = (from SkillCardsModel item in result
                          where item.Is_Basic == true
                          select item);
            return result;
        }
    }
}
