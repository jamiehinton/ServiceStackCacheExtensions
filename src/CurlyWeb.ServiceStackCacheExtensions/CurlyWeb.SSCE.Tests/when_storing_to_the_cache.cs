namespace CurlyWeb.SSCE.Tests
{
  using System;
  using System.Collections.Generic;
  using NSubstitute;
  using NUnit.Framework;
  using ServiceStack.CacheAccess;
  using ServiceStackCacheExtensions;

  [ TestFixture ]
  public class When_storing_to_the_cache
  {
    [ Test ]
    public void Should_store_if_not_already_found()
    {
      var cacheClient = Substitute.For< ICacheClient >();
      cacheClient.Get< IList< string > >( "bob" ).Returns( x => null );
      cacheClient.GetOrSet( "bob", () => new Bob().GetBobs(), TimeSpan.FromHours( 1 ) );
      cacheClient.Received().Set( Arg.Is( "bob" ), Arg.Any< IList< string > >(), Arg.Any<TimeSpan>() );
    }

    [ Test ]
    public void Should_return_from_cache_if_already_there()
    {
      var cacheClient = Substitute.For< ICacheClient >();
      cacheClient.Get< IList< string > >( "bob" ).Returns( x => new List< string >() );
      cacheClient.GetOrSet( "bob", () => new Bob().GetBobs(), TimeSpan.FromHours( 1 ) );
      cacheClient.DidNotReceive().Set( Arg.Is( "bob" ), Arg.Any< IList< string > >(), Arg.Any< TimeSpan >() );
    }
  }

  internal class Bob
  {
    public IList< string > GetBobs()
    {
      return new List< string >();
    }
  }
}