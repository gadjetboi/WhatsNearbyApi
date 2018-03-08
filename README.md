# WhatsNearbyApi
Asp.Net Core 2, Asp.Net Identity, Entity Framework, JWT (Json Web Token), AutoMapper, Yelp API, SQLite

# Register
URL: /api/auth/register
<br />
Method: POST
<br />
Body:

	{
		"username": "vanzp@myemail.com",
		"firstname": "Vanz",
		"gender": "Male",
		"lastname": "P"
	}
Response: Status Code 201

# Request Token
URL: /api/auth/token
<br />
Method: POST
<br />
Body:

	{
		"username": "vanzp@myemail.com",
		"password": "P@ssword123"
	}

Response: 

	{
		"token": string,
		"expiration": datetime
	}

# Get Businesses
URL: /api/yelp/GetBusiness?lat=37.786882&lng=-122.399972&categories=restaurants
<br />
Method: GET
<br />
Headers: 

    {
	    "Authorization": Bearer token
    }

Response: 

	{
		region: object,
		total: int,
		businesses: object[]
	}