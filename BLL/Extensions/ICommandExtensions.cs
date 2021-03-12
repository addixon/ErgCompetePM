using BO;
using BO.Interfaces;
using System;
using System.Reflection;

namespace BLL.Extensions
{
    public static class ICommandExtensions
    {

        public static void UpdatePMData(this ICommand command, PMData pmData)
        {
            if (pmData == null)
            {
                return;
            }

            if (command.Value == null)
            {
                return;
            }

            PropertyInfo? property = pmData.GetType().GetProperty(command.Name);

            if (property == null)
            {
                return;
            }

            if (Nullable.GetUnderlyingType(property.PropertyType) != command.Value.GetType())
            {
                return;
            }

            property.SetValue(pmData, command.Value);
        }
    }
}