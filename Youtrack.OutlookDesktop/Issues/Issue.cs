//using System;
//using System.Collections.Generic;
//using System.Dynamic;
//using System.Globalization;
//using System.Linq;

//using RestSharp.Extensions;

//namespace Youtrack.OutlookDesktop.Issues
//{
//    public class Issue : DynamicObject
//    {
//        private readonly IDictionary<string, object> _allFields = new Dictionary<string, object>(StringComparer.InvariantCultureIgnoreCase);
//        private string _id;

//        public string Id
//        {
//            get { return _id ?? (_id = (string) _allFields["Id"]); }
//        }

//        public ExpandoObject ToExpandoObject()
//        {
//            IDictionary<string, object> expando = new ExpandoObject();

//            foreach (dynamic field in _allFields)
//            {
//                if (String.Compare(field.Key, "ProjectShortName", StringComparison.InvariantCultureIgnoreCase) == 0)
//                    expando.Add("project", field.Value);
//                else if (String.Compare(field.Key, "permittedGroup", StringComparison.InvariantCultureIgnoreCase) == 0)
//                    expando.Add("permittedGroup", field.Value);
//                else
//                    expando.Add(field.Key.ToLower(), field.Value);
//            }
//            return (ExpandoObject) expando;
//        }

//        public override bool TryGetMember(GetMemberBinder binder, out object result)
//        {
//            if (_allFields.ContainsKey(binder.Name))
//            {
//                result = _allFields[binder.Name];
//                return true;
//            }
//            return base.TryGetMember(binder, out result);
//        }

//        public override bool TrySetMember(SetMemberBinder binder, object value)
//        {
//            if (String.Compare(binder.Name, "field", StringComparison.OrdinalIgnoreCase) == 0)
//            {
//                foreach (var val in (IEnumerable<dynamic>) value)
//                {

//                    object objectValue;
//                    object[] arrayValue =  val.value as object[];
//                    if (arrayValue != null)
//                    {
//                        objectValue = arrayValue.Length == 0
//                                ? null
//                                : arrayValue[0];
//                    }
//                    else
//                    {
//                        objectValue = val.value;
//                    }

//                    try
//                    {
//                        string valName = val.name;
//                        _allFields[valName
//                                .ToPascalCase(CultureInfo.InvariantCulture)] = objectValue;
//                    }
//                    catch (Exception) {}


//                }
//                return true;
//            }
//            _allFields[binder.Name.ToPascalCase(CultureInfo.InvariantCulture)] = value;
//            return true;
//        }

//    }
//}