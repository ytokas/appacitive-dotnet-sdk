﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appacitive.Sdk
{
    public class PropertyExpression
    {
        internal PropertyExpression(string name)
        {
            this.Field = name;
        }

        public string Field { get; private set; }

        internal IFieldValue Value { get; set; }

        public IQuery IsEqualTo(string value)
        {
            // Hack!!! __id mapping to be fixed inside API.
            if( this.Field.Equals("__id", StringComparison.OrdinalIgnoreCase) == true )
                return FieldQuery.IsEqualTo(FieldType.Property, this.Field, long.Parse(value));
            return FieldQuery.IsEqualTo(FieldType.Property, this.Field,  StringUtils.EscapeSingleQuotes(value));
        }

        public IQuery IsEqualTo(bool value)
        {
            return FieldQuery.IsEqualTo(FieldType.Property, this.Field, value);
        }

        public IQuery IsEqualTo(long value)
        {
            return FieldQuery.IsEqualTo(FieldType.Property, this.Field, value);
        }

        public IQuery IsEqualTo(decimal value)
        {
            return FieldQuery.IsEqualTo(FieldType.Property, this.Field, value);
        }

        public IQuery IsEqualTo(DateTime value)
        {
            return FieldQuery.IsEqualTo(FieldType.Property, this.Field, value);
        }

        public IQuery IsGreaterThan(long value)
        {
            return FieldQuery.IsGreaterThan(FieldType.Property, this.Field, value);
        }

        public IQuery IsGreaterThan(decimal value)
        {
            return FieldQuery.IsGreaterThan(FieldType.Property, this.Field, value);
        }

        public IQuery IsGreaterThan(DateTime value)
        {
            return FieldQuery.IsGreaterThan(FieldType.Property, this.Field, value);
        }

        public IQuery IsGreaterThanEqualTo(long value)
        {
            return FieldQuery.IsGreaterThanEqualTo(FieldType.Property, this.Field, value);
        }

        public IQuery IsGreaterThanEqualTo(decimal value)
        {
            return FieldQuery.IsGreaterThanEqualTo(FieldType.Property, this.Field, value);
        }

        public IQuery IsGreaterThanEqualTo(DateTime value)
        {
            return FieldQuery.IsGreaterThanEqualTo(FieldType.Property, this.Field, value);
        }

        public IQuery IsLessThan(long value)
        {   
            return FieldQuery.IsLessThan(FieldType.Property, this.Field, value);
        }

        public IQuery FreeTextMatches(string freeTextExpression)
        {
            return FieldQuery.FreeTextMatches(FieldType.Property, this.Field, StringUtils.EscapeSingleQuotes(freeTextExpression));
        }



        public IQuery IsLessThan(decimal value)
        {
            return FieldQuery.IsLessThan(FieldType.Property, this.Field, value);
        }

        public IQuery IsLessThan(DateTime value)
        {
            return FieldQuery.IsLessThan(FieldType.Property, this.Field, value);
        }

        public IQuery IsLessThanEqualTo(long value)
        {
            return FieldQuery.IsLessThanEqualTo(FieldType.Property, this.Field, value);
        }

        public IQuery IsLessThanEqualTo(decimal value)
        {
            return FieldQuery.IsLessThanEqualTo(FieldType.Property, this.Field, value);
        }

        public IQuery IsLessThanEqualTo(DateTime value)
        {
            return FieldQuery.IsLessThanEqualTo(FieldType.Property, this.Field, value);
        }

        public IQuery Like(string value)
        {
            return FieldQuery.Like(FieldType.Property, this.Field, StringUtils.EscapeSingleQuotes(value));
        }

        public IQuery StartsWith(string value)
        {
            return FieldQuery.StartsWith(FieldType.Property, this.Field, StringUtils.EscapeSingleQuotes(value));
        }

        public IQuery EndsWith(string value)
        {
            return FieldQuery.EndsWith(FieldType.Property, this.Field, StringUtils.EscapeSingleQuotes(value));
        }

        public IQuery WithinCircle(Geocode center, decimal radius, DistanceUnit unit = DistanceUnit.Miles)
        {
            return new RadialSearchQuery(this.Field, center, radius, unit);
        }

        public IQuery WithinPolygon(IEnumerable<Geocode> points)
        {
            return new PolygonSearchQuery(this.Field, points);
        }

        public IQuery WithinPolygon(params Geocode[] points)
        {
            return new PolygonSearchQuery(this.Field, points);
        }

        public IQuery Between(decimal before, decimal after)
        {
            return BetweenQuery.Between(FieldType.Property, this.Field, before, after);
        }

        public IQuery Between(long before, long after)
        {
            return BetweenQuery.Between(FieldType.Property, this.Field, before, after);
        }

        public IQuery Between(DateTime before, DateTime after)
        {
            return BetweenQuery.Between(FieldType.Property, this.Field, before, after);
        }
    }
}
