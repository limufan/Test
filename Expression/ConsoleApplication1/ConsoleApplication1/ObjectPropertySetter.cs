using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class ObjectPropertySetter
    {
        public ObjectPropertySetter(Type type)
        {
            this._type = type;
            this._typedActionTypes = new List<Type>();
            this._typedActionTypes.Add(typeof(int));

            this.InitActions();
            this.InitFuncs();
        }

        Type _type;

        List<Type> _typedActionTypes;
        Action<object, string, object> _objectSetAction;
        Action<object, string, int> _intSetAction;
        Func<object, string, object> _objectGetFunc;
        Func<object, string, int> _intGetFunc;

        public void Set(object instance, string propertyName, object propertyValue)
        {
            this._objectSetAction(instance, propertyName, propertyValue);
        }

        public void Set(object instance, string propertyName, int propertyValue)
        {
            this._intSetAction(instance, propertyName, propertyValue);
        }

        public int GetIntValue(object instance, string propertyName)
        {
            return this._intGetFunc(instance, propertyName);
        }

        public object GetObjectValue(object instance, string propertyName)
        {
            return this._objectGetFunc(instance, propertyName);
        }

        private void InitActions()
        {
            PropertyInfo[] properties = this._type.GetProperties().Where(p => !this._typedActionTypes.Contains(p.PropertyType)).ToArray();
            this._objectSetAction = this.CreateAction<object>(properties);

            properties = this._type.GetProperties().Where(p => p.PropertyType == typeof(int)).ToArray();
            this._intSetAction = this.CreateAction<int>(properties);
        }

        private Action<object, string, MapType> CreateAction<MapType>(PropertyInfo[] properties)
        {
            var instanceParam = Expression.Parameter(typeof(object), "instance");
            var propertyNameParam = Expression.Parameter(typeof(string), "propertyName");
            var valueParam = Expression.Parameter(typeof(MapType), "value");

            List<SwitchCase> cases = new List<SwitchCase>();
            
            foreach (PropertyInfo property in properties)
            {
                Expression propertyTypeParameter = valueParam;
                if (typeof(MapType) != property.PropertyType)
                {
                    propertyTypeParameter = Expression.Convert(valueParam, property.PropertyType);
                }
                var typeParameter = Expression.Convert(instanceParam, this._type);

                var callMethodExpression = Expression.Call(typeParameter, property.SetMethod, propertyTypeParameter);
                var switchCase = Expression.SwitchCase(
                    callMethodExpression,
                    Expression.Constant(property.Name)
                );
                cases.Add(switchCase);
            }

            var switchExpression = Expression.Switch(propertyNameParam, cases.ToArray());

            return Expression.Lambda<Action<object, string, MapType>>(switchExpression, instanceParam, propertyNameParam, valueParam)
                    .Compile();
        }

        private void InitFuncs()
        {
            PropertyInfo[] properties = this._type.GetProperties().Where(p => !this._typedActionTypes.Contains(p.PropertyType)).ToArray();
            this._objectGetFunc = this.CreateFunc<object>(properties);

            properties = this._type.GetProperties().Where(p => p.PropertyType == typeof(int)).ToArray();
            this._intGetFunc = this.CreateFunc<int>(properties);
        }

        private Func<object, string, PropertyType> CreateFunc<PropertyType>(PropertyInfo[] properties)
        {
            List<Expression> blockExpressions = new List<Expression>();

            LabelTarget propertyTypeLabelTarget = Expression.Label(typeof(PropertyType));
            var valueVar = Expression.Variable(typeof(PropertyType), "value");
            blockExpressions.Add(valueVar);

            var instanceParam = Expression.Parameter(typeof(object), "instance");
            var propertyNameParam = Expression.Parameter(typeof(string), "propertyName");

            List<SwitchCase> cases = new List<SwitchCase>();

            foreach (PropertyInfo property in properties)
            {
                var typeParameter = Expression.Convert(instanceParam, this._type);

                var callMethodExpression = Expression.Call(typeParameter, property.GetMethod);
                Expression getValueExpression = callMethodExpression;
                if (typeof(PropertyType) != property.PropertyType)
                {
                    getValueExpression = Expression.Convert(callMethodExpression, typeof(PropertyType));
                }
                var valueAssign = Expression.Assign(valueVar, getValueExpression);
                var switchCase = Expression.SwitchCase(
                    valueAssign,
                    Expression.Constant(property.Name)
                );
                cases.Add(switchCase);
            }

            var defaultValue = Expression.Default(typeof(PropertyType));

            var switchExpression = Expression.Switch(propertyNameParam, Expression.Assign(valueVar, defaultValue), cases.ToArray());
            blockExpressions.Add(switchExpression);
            blockExpressions.Add(valueVar);

            BlockExpression block = Expression.Block(new ParameterExpression[] { valueVar }, blockExpressions);
            
            return Expression.Lambda<Func<object, string, PropertyType>>(block, instanceParam, propertyNameParam)
                    .Compile();
        }
    }
}
