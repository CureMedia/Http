# Http

# Build

| Branch | Status | Packages |
|--------|--------|-------|
| master | [![Build status](https://ci.appveyor.com/api/projects/status/0q8713ajosu68d2s/branch/master?svg=true)](https://ci.appveyor.com/project/curemedia/http/branch/master) |  |
| develop | [![Build status](https://ci.appveyor.com/api/projects/status/0q8713ajosu68d2s?svg=true)](https://ci.appveyor.com/project/curemedia/http) | [MyGet](https://www.myget.org/feed/Packages/curemedia-ci) |

# OAuth2
Communicating with a API protected by OAuth2 requires setting the proper access method to send the access token, handle expiration of tokens as well renewal - either by requesting a new or use a refresh token.

The flow is

1. Retrieve token prior making request
1. Control token is valid (either exists or has expired). If not, initiate renewal negoitation.
1. Set bearer access token as decsribed by [RFC6750](https://tools.ietf.org/html/rfc6750#section-2)
1. Execute the request. If HTTP status code is *not* Unauthorized, then break execution and return response.
1. Initiate renewal negoitation. If failed to renew, return response.
1. Dipose previous response
1. Set bearer access token.
1. Execute the request and return response.

_TODO_ Make sure their is a service to handle [WWW-Authenticate](https://tools.ietf.org/html/rfc6750#section-3) response header field and take appropriate actions based on return.
Default implementation should just be a no-op.
