﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace Org.Reddragonit.BackBoneDotNet
{
    //thrown when no routes to a given model were specified by attributes
    [Serializable]
    public class NoRouteException : Exception
    {
        public NoRouteException(Type t)
            : base("The IModel type " + t.FullName + " is not valid as no Model Route has been specified.") { }

        protected NoRouteException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }

    //thrown when more than one model is mapped to the same route
    [Serializable]
    public class DuplicateRouteException : Exception
    {
        public DuplicateRouteException(string path1, Type type1, string path2, Type type2)
            : base("The IModel type "+type2.FullName+" is not valid as its route "+path2+" is a duplicate for the route "+path1+" contained within the Model "+type1.FullName) { }

        protected DuplicateRouteException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }

    //thrown when more than one Load method exists in a given model
    [Serializable]
    public class DuplicateLoadMethodException : Exception
    {
        public DuplicateLoadMethodException(Type t, string methodName)
            : base("The IModel type " + t.FullName + " is not valid because the method " + methodName + " is tagged as a load method when a valid load method already exists.") { }

        protected DuplicateLoadMethodException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }

    //thrown when the return type of a load method is not of the model or of the models inheritance
    [Serializable]
    public class InvalidLoadMethodReturnType : Exception
    {
        public InvalidLoadMethodReturnType(Type t, string methodName)
            : base("The IModel type "+t.FullName+" is not valid because the method "+methodName+" does not return a valid type for loading.")
        { }

        protected InvalidLoadMethodReturnType(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }

    //thrown when no Load method is specified
    [Serializable]
    public class NoLoadMethodException : Exception
    {
        public NoLoadMethodException(Type t)
            : base("The IModel type " + t.FullName + " is not valid because there is no valid load method found.  A Load method must have the attribute ModelLoadMethod() as well as be similar to public static IModel Load(string id).") { }

        protected NoLoadMethodException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }

    //special exception designed to house all found validation exceptions
    [Serializable]
    public class ModelValidationException : Exception
    {
        private List<Exception> _innerExceptions;
        public List<Exception> InnerExceptions
        {
            get { return _innerExceptions; }
        }

        public ModelValidationException(List<Exception> exceptions)
            : base("Model Definition Validations have failed.")
        {
            _innerExceptions = exceptions;
        }

        protected ModelValidationException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }

    //thrown when the id property of the model is tagged as block
    [Serializable]
    public class ModelIDBlockedException : Exception
    {
        public ModelIDBlockedException(Type t)
            : base("The IModel type " + t.FullName + " is not valid because the ID property has been tagged with ModelIgnoreProperty.") { }

        protected ModelIDBlockedException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }

    //thrown when no empty constructor is specifed but adding the model has not been blocked
    [Serializable]
    public class NoEmptyConstructorException : Exception
    {
        public NoEmptyConstructorException(Type t)
            :base("The IModel type "+t.FullName+" is not valid because it does not block adding and has no empty constructor.")
        {
        }

        protected NoEmptyConstructorException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }

    //thrown when the return type is not valid for a ModelSelectList function
    [Serializable]
    public class InvalidModelSelectOptionValueReturnException : Exception
    {
        public InvalidModelSelectOptionValueReturnException(Type t, MethodInfo mi)
            : base("The IModel type "+t.FullName+" is not valid because the ModelSelectList function "+mi.Name+" does not return a valid type (List<sModelSelectOptionValue> or sModelSelectOptionValue[]).")
        { }

        protected InvalidModelSelectOptionValueReturnException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }

    //thrown when the ModelSelectList function is not static
    [Serializable]
    public class InvalidModelSelectStaticException : Exception
    {
        public InvalidModelSelectStaticException(Type t, MethodInfo mi)
            : base("The IModel type " + t.FullName + " is not valid because the ModelSelectList function " + mi.Name + " is not static.")
        { }

        protected InvalidModelSelectStaticException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }

    //thrown when no model select method is specified but another model uses that type as a property
    [Serializable]
    public class NoModelSelectMethodException : Exception
    {
        public NoModelSelectMethodException(Type t, PropertyInfo pi)
            : base("The IModel type " + t.FullName + " is not valid because the property " + pi.Name + " is linked to an IModel " + pi.PropertyType.FullName + " does not have a load select method.")
        { }

        protected NoModelSelectMethodException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }

    //thrown when more than one model select list function is specified
    [Serializable]
    public class MultipleSelectOptionValueMethodsException : Exception{
        public MultipleSelectOptionValueMethodsException(Type t,MethodInfo mi)
            : base("The IModel type "+t.FullName+" is not valid because there is more than one ModelSelectList load function, the additional function declared is "+mi.Name+".")
        {}

        protected MultipleSelectOptionValueMethodsException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }

    //thrown when the return type for the ModelListMethod function is not valid
    [Serializable]
    public class InvalidModelListMethodReturnException : Exception{
        public InvalidModelListMethodReturnException(Type t, MethodInfo mi)
            : base("The IModel type " + t.FullName + " is not valid because the return type for the model list method " + mi.Name + " is not either List<" + t.FullName + "> or " + t.FullName + "[].")
        { }

        protected InvalidModelListMethodReturnException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }

    //thrown when the path specified does not contain the proper number of method parameters
    [Serializable]
    public class InvalidModelListParameterCountException : Exception
    {
        public InvalidModelListParameterCountException(Type t, MethodInfo mi, string path)
            : base("The IModel type " + t.FullName + " is not valid because the number of parameters for the method " + mi.Name + " does not match the number of variables in the path " + path)
        { }

        protected InvalidModelListParameterCountException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }

    //thrown when the parameter of a ModelListMethod is not a usable parameter
    [Serializable]
    public class InvalidModelListParameterTypeException : Exception
    {
        public InvalidModelListParameterTypeException(Type t, MethodInfo mi, ParameterInfo pi)
            : base("The IModel type " + t.FullName + " is not valid because the parameter " + pi.Name + " in the method " + mi.Name + " is not a usable parameter for a ModelListMethod.")
        { }

        protected InvalidModelListParameterTypeException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }

    //thrown when a parameter used for paging a model list is not a valid type of parameter
    [Serializable]
    public class InvalidModelListPageParameterTypeException : Exception
    {
        public InvalidModelListPageParameterTypeException(Type t, MethodInfo mi, ParameterInfo pi)
            : base("The IModel type " + t.FullName + " is not valid because the parameter " + pi.Name + " in the method " + mi.Name + " is not a usable as a paging parameter for a ModelListMethod.")
        {}

        protected InvalidModelListPageParameterTypeException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }

    //thrown when the parameter to indicate the total pages in a paged model list is not an out parameter
    [Serializable]
    public class InvalidModelListPageTotalPagesNotOutException : Exception{
        public InvalidModelListPageTotalPagesNotOutException(Type t, MethodInfo mi, ParameterInfo pi)
            : base("The IModel type " + t.FullName + " is not valid because the parameter " + pi.Name + " in the method " + mi.Name + " is not an out parameter which is needed to indicate the total number of pages.")
        {}

        protected InvalidModelListPageTotalPagesNotOutException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }

    //thrown when the parameter of a ModelListMethod is an out parameter and it is not a paged call
    [Serializable]
    public class InvalidModelListParameterOutException : Exception
    {
        public InvalidModelListParameterOutException(Type t, MethodInfo mi, ParameterInfo pi)
            : base("The IModel type " + t.FullName + " is not valid because the parameter " + pi.Name + " in the method " + mi.Name + " is an out parameter.")
        { }

        protected InvalidModelListParameterOutException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }

    //thrown when multiple ModelListMethod are delcared and 1 or more but not all are declared as paged
    [Serializable]
    public class InvalidModelListNotAllPagedException : Exception
    {
        public InvalidModelListNotAllPagedException(Type t, MethodInfo mi, string path)
            : base("The IModel type " + t.FullName + " is not valid because ModelListMethod for the path "+path+" is not marked as paged likst the others.")
        { }

        protected InvalidModelListNotAllPagedException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }

    //thrown when the name in a model view attribute is the same as another one specified
    [Serializable]
    public class RepeatedAttributeTagName : Exception
    {
        public RepeatedAttributeTagName(Type t, string tag)
            : base("The IModel type "+t.FullName+" is not valid because the ModelViewAttribute tag "+tag+" has already been specifed by another ModelViewAttribute.")
        {}

        protected RepeatedAttributeTagName(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }

    //thrown when the ModelViewAttribute specifies class as the tag name instead of using ModelViewClass
    [Serializable]
    public class InvalidAttributeTagName : Exception
    {
        public InvalidAttributeTagName(Type t)
            : base("The IModel type " + t.FullName + " is not valid because the ModelViewAttribute specified a tag of class instead of using ModelViewClass.")
        { }

        protected InvalidAttributeTagName(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }

    //thrown when the ModelSaveMethod Attribute is specified more than once in the Model
    [Serializable]
    public class DuplicateModelSaveMethodException : Exception
    {
        public DuplicateModelSaveMethodException(Type t, MethodInfo mi)
            :base("The IModel type "+t.FullName+" is not valid because the ModelSaveMethod is specified on the method "+mi.Name+" as well as another method.")
        {}

        protected DuplicateModelSaveMethodException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }

    //thrown when the ModelDeleteMethod Attribute is specified more than once in the Model
    [Serializable]
    public class DuplicateModelDeleteMethodException : Exception
    {
        public DuplicateModelDeleteMethodException(Type t, MethodInfo mi)
            : base("The IModel type " + t.FullName + " is not valid because the ModelDeleteMethod is specified on the method " + mi.Name + " as well as another method.")
        { }

        protected DuplicateModelDeleteMethodException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }

    //thrown when the ModelUpdateMethod Attribute is specified more than once in the Model
    [Serializable]
    public class DuplicateModelUpdateMethodException : Exception
    {
        public DuplicateModelUpdateMethodException(Type t, MethodInfo mi)
            : base("The IModel type " + t.FullName + " is not valid because the ModelUpdateMethod is specified on the method " + mi.Name + " as well as another method.")
        { }

        protected DuplicateModelUpdateMethodException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }

    //thrown when the ModelSaveMethod Attribute is specified more than once in the Model
    [Serializable]
    public class InvalidModelSaveMethodException : Exception
    {
        public InvalidModelSaveMethodException(Type t, MethodInfo mi)
            : base("The IModel type " + t.FullName + " is not valid because the method "+mi.Name+" is not of the pattern public bool Save() for ModelSaveMethod.")
        { }

        protected InvalidModelSaveMethodException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }

    //thrown when the ModelDeleteMethod Attribute is specified more than once in the Model
    [Serializable]
    public class InvalidModelDeleteMethodException : Exception
    {
        public InvalidModelDeleteMethodException(Type t, MethodInfo mi)
            : base("The IModel type " + t.FullName + " is not valid because the method " + mi.Name + " is not of the pattern public bool Delete() for ModelDeleteMethod.")
        { }

        protected InvalidModelDeleteMethodException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }

    //thrown when the ModelUpdateMethod Attribute is specified more than once in the Model
    [Serializable]
    public class InvalidModelUpdateMethodException : Exception
    {
        public InvalidModelUpdateMethodException(Type t, MethodInfo mi)
            : base("The IModel type " + t.FullName + " is not valid because the method " + mi.Name + " is not of the pattern public bool Update() for ModelUpdateMethod.")
        { }

        protected InvalidModelUpdateMethodException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }

    //thrown when an ExposedMethod will have the same javascript signature as another for a model
    [Serializable]
    public class DuplicateMethodSignatureException : Exception
    {
        public DuplicateMethodSignatureException(Type t, MethodInfo mi)
            : base(string.Format("The IModel type {0} is not valid because the method {1} has a javascript signature identical to a previously detected method of the same same.",
            t.FullName,
            mi.Name)) { }

        protected DuplicateMethodSignatureException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
