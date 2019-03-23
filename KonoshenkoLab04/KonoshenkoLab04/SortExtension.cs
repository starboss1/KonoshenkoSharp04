using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using KMA.ProgrammingInCSharp2019.KonoshenkoLab04.Models;

namespace KMA.ProgrammingInCSharp2019.KonoshenkoLab04
{
    internal static class SortExtension
    {
        public static readonly string[] SortFilterOptions =
            Array.ConvertAll(typeof(Person).GetProperties(), (property) => property.Name);

        public static List<Person> SortByProperty(this List<Person> persons, string property, bool ascending)
        {
            var res = Array.IndexOf(SortFilterOptions, property) >= 0
                ? (ascending
                    ? (from p in persons
                        orderby p.GetType().GetProperty(property)?.GetValue(p, null)
                        select p).ToList()
                    : (from p in persons
                        orderby p.GetType().GetProperty(property)?.GetValue(p, null) descending
                        select p).ToList())
                : persons;
            return res;
        }

        public static List<Person> FilterByProperty(this List<Person> persons, string property, string query)
        {
            if (Array.IndexOf(SortFilterOptions, property) < 0)
                return new List<Person>();
            query = query.ToLower();
            return (from p in persons
                where (p.GetType().GetProperty(property)?.GetValue(p, null)).ToString().ToLower().Contains(query)
                select p).ToList();
        }
    }
}
