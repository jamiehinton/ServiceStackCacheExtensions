namespace CurlyWeb.ServiceStackCacheExtensions
{
  using System;
  using ServiceStack.CacheAccess;

  public static class CacheClientExtensionMethods
  {
    public static T GetOrSet< T >( this ICacheClient cacheClient, string key, Func< T > expression, TimeSpan expiresIn )
    {
      var found = cacheClient.Get< T >( key );
      if( !Equals( found, default( T ) ) )
        return found;

      var executed = expression.Invoke();
      cacheClient.Set( key, executed, expiresIn );
      return executed;
    }
  }
}