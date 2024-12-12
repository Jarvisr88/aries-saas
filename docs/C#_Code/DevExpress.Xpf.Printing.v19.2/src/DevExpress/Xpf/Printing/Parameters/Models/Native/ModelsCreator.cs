namespace DevExpress.Xpf.Printing.Parameters.Models.Native
{
    using DevExpress.Data.Browsing;
    using DevExpress.DocumentServices.ServiceModel;
    using DevExpress.Utils;
    using DevExpress.Xpf.Printing.Parameters;
    using DevExpress.Xpf.Printing.Parameters.Models;
    using DevExpress.XtraPrinting.Native;
    using DevExpress.XtraReports.Parameters;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;

    public class ModelsCreator
    {
        private static ParameterModel CreateParameterModel(IClientParameter parameter)
        {
            ClientParameter parameter2 = (ClientParameter) parameter;
            LookUpValueCollection lookUpValues = (parameter2.OriginalParameter.LookUpSettings is StaticListLookUpSettings) ? ((StaticListLookUpSettings) parameter2.OriginalParameter.LookUpSettings).LookUpValues : null;
            ParameterModel model = ParameterModel.CreateParameterModel(parameter2.OriginalParameter, lookUpValues);
            model.Path = parameter2.Path;
            model.IsFilteredLookUpSettings = parameter2.IsFilteredLookUpSettings;
            return model;
        }

        private static ParameterModel CreateParameterModel(Parameter parameter, DataContext dataContext, ParameterValueProvider valueProvider)
        {
            LookUpSettings lookUpSettings = GetLookUpSettings(parameter);
            LookUpValueCollection lookUpValues = LookUpHelper.GetLookUpValues(lookUpSettings, dataContext, valueProvider);
            ParameterModel model = ParameterModel.CreateParameterModel(parameter, lookUpValues);
            if (lookUpSettings != null)
            {
                model.IsFilteredLookUpSettings = !string.IsNullOrEmpty(lookUpSettings.FilterString);
            }
            return model;
        }

        public static List<ParameterModel> CreateParameterModels(IEnumerable<IClientParameter> parameters)
        {
            List<ParameterModel> result = new List<ParameterModel>();
            if ((parameters != null) && (parameters.Count<IClientParameter>() != 0))
            {
                parameters.ForEach<IClientParameter>(delegate (IClientParameter x) {
                    result.Add(CreateParameterModel(x));
                });
            }
            return result;
        }

        public static List<ParameterModel> CreateParameterModels(IEnumerable<Parameter> parameters, DataContext dataContext)
        {
            List<ParameterModel> result = new List<ParameterModel>();
            if ((parameters != null) && (parameters.Count<Parameter>() != 0))
            {
                ParameterValueProvider valueProvider = new ParameterValueProvider(result);
                parameters.ForEach<Parameter>(delegate (Parameter x) {
                    result.Add(CreateParameterModel(x, dataContext, valueProvider));
                });
            }
            return result;
        }

        private static LookUpSettings GetLookUpSettings(Parameter parameter)
        {
            if (parameter.LookUpSettings != null)
            {
                return parameter.LookUpSettings;
            }
            if (!parameter.Type.IsEnum)
            {
                return null;
            }
            StaticListLookUpSettings settings = new StaticListLookUpSettings();
            TypeConverter converter = TypeDescriptor.GetConverter(parameter.Type);
            foreach (object obj2 in Enum.GetValues(parameter.Type))
            {
                string enumItemDisplayText = EnumExtensions.GetEnumItemDisplayText(obj2);
                settings.LookUpValues.Add(new LookUpValue(obj2, enumItemDisplayText));
            }
            return settings;
        }
    }
}

