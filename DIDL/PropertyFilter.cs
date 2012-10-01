using System.Collections.Generic;

namespace MediaPortal.Extensions.MediaServer.DIDL
{
    public class PropertyFilter
    {
        public bool FilterNothing { get; private set; }
        public IList<string> AllowedList { get; private set; }
        public string ElementBase { get; private set; }

        public PropertyFilter(string filter)
        {
            AllowedList = new List<string>();
            if (filter == "*" || filter == string.Empty)
            {
                FilterNothing = true;
            }
            else
            {
                var items = filter.Split(',');
                foreach (var item in items)
                {
                    AllowedList.Add(item.Trim());
                }
            }
        }

        public bool IsAllowed(string item)
        {
            item = ElementBase + item;
            return FilterNothing || AllowedList.Contains(item);
        }

        public PropertyFilter CloneWithElementBase(string namespaceBase, string elementBase)
        {
            if (namespaceBase != string.Empty) namespaceBase += ":";
            var filter = new PropertyFilter(string.Empty)
                             {
                                 ElementBase = namespaceBase + elementBase,
                                 AllowedList = AllowedList,
                                 FilterNothing = FilterNothing
                             };
            return filter;
        }
    }
}
