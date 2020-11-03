using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;

namespace _2_1_Call_MSGraph.Controllers
{
    public class HomeHelper
    {
        public bool IsAdmin(Microsoft.Graph.IUserMemberOfCollectionWithReferencesPage mygroups)
        {
            bool result = false;

            SortedDictionary<string, string> groups = new SortedDictionary<string, string>();

            groups = GroupsToDictionary(groups, mygroups);

            foreach (KeyValuePair<string, string> kvp in groups)
            {
                if (kvp.Key.Contains("_admin"))
                {
                    result = true;
                }
            }

            return result;
        }

        public SortedDictionary<string, string> GroupsToDictionary(SortedDictionary<string, string> groups, Microsoft.Graph.IUserMemberOfCollectionWithReferencesPage mygroups)
        {
            foreach (var group in mygroups)
            {
                var properties = group.GetType().GetProperties();
                string name = "";
                string id = "";

                foreach (var child in properties)
                {
                    if (child.Name == "DisplayName" || child.Name == "Id")
                    {

                        object value = child.GetValue(group);
                        string stringRepresentation;
                        if (!(value is string) && value is IEnumerable<string>)
                        {
                            stringRepresentation = "["
                                + string.Join(", ", (value as IEnumerable<string>).OfType<object>().Select(c => c.ToString()))
                                + "]";
                        }
                        else
                        {
                            stringRepresentation = value?.ToString();
                        }

                        if (child.Name == "DisplayName")
                        {
                            name = stringRepresentation;
                        }
                        else if (child.Name == "Id")
                        {
                            id = stringRepresentation;
                        }
                    }
                }

                if (name != "" && id != "")
                {
                    if (!groups.ContainsKey(name))
                    {
                        groups.Add(name, id);
                    }
                }
            }

            return groups;
        }

        public SortedDictionary<string, string> TransitiveGroupsToDictionary(SortedDictionary<string, string> groups, Microsoft.Graph.IUserTransitiveMemberOfCollectionWithReferencesPage transitivegroups)
        {
            foreach (var group in transitivegroups)
            {
                var properties = group.GetType().GetProperties();
                string name = "";
                string id = "";

                foreach (var child in properties)
                {
                    if (child.Name == "DisplayName" || child.Name == "Id")
                    {

                        object value = child.GetValue(group);
                        string stringRepresentation;
                        if (!(value is string) && value is IEnumerable<string>)
                        {
                            stringRepresentation = "["
                                + string.Join(", ", (value as IEnumerable<string>).OfType<object>().Select(c => c.ToString()))
                                + "]";
                        }
                        else
                        {
                            stringRepresentation = value?.ToString();
                        }

                        if (child.Name == "DisplayName")
                        {
                            name = stringRepresentation;
                        }
                        else if (child.Name == "Id")
                        {
                            id = stringRepresentation;
                        }
                    }
                }

                if (name != "" && id != "")
                {
                    if (!groups.ContainsKey(name))
                    {
                        groups.Add(name, id);
                    }
                }
            }

            return groups;
        }
        public SortedDictionary<string, string> UsersToDictionary(SortedDictionary<string, string> users, Microsoft.Graph.IGroupMembersCollectionWithReferencesPage members)
        {
            foreach (var member in members)
            {
                if (member.GetType().ToString() == "Microsoft.Graph.User")
                {
                    var properties = member.GetType().GetProperties();
                    string name = "";
                    string id = "";

                    foreach (var child in properties)
                    {
                        if (child.Name == "DisplayName" || child.Name == "Id")
                        {

                            object value = child.GetValue(member);
                            string stringRepresentation;
                            if (!(value is string) && value is IEnumerable<string>)
                            {
                                stringRepresentation = "["
                                    + string.Join(", ", (value as IEnumerable<string>).OfType<object>().Select(c => c.ToString()))
                                    + "]";
                            }
                            else
                            {
                                stringRepresentation = value?.ToString();
                            }

                            if (child.Name == "DisplayName")
                            {
                                name = stringRepresentation;
                            }
                            else if (child.Name == "Id")
                            {
                                id = stringRepresentation;
                            }
                        }
                    }

                    if (name != "" && id != "")
                    {
                        if (!users.ContainsKey(name))
                        {
                            users.Add(name, id);
                        }
                    }
                }
            }

            return users;
        }

    }
}
