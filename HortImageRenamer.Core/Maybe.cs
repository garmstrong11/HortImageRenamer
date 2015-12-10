namespace HortImageRenamer.Core
{
  using System;
  using System.Collections;
  using System.Collections.Generic;

  public class Maybe<T> : IEnumerable<T> where T : class
  {
    private readonly IEnumerable<T> _values;

    public Maybe()
    {
      _values = new T[0];
    }

    public Maybe(T value)
    {
      if (value == null) throw new ArgumentNullException("value");
      _values = new[] {value};
    }

    #region Implementation of IEnumerable

    public IEnumerator<T> GetEnumerator()
    {
      return _values.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return GetEnumerator();
    }

    #endregion
  }
}