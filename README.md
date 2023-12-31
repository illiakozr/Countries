﻿# Countries API
This application provides functionality of retrieving countries details.<br/>

**CountriesController** <br/>
This controller provides an endpoint to get countries information, filtered by parameters.<br/>

GET api/countries endpoint: Return the list of all countries in JSON format.<br/>
Possible filter values:<br/>
**name** - the country common name.<br/>
**population** - filter countries by population. The value goes in millions.<br/>
**sort** - sort countries. Only 'descend' or 'ascend' allowed.<br/>
**pagesize** - limit number of countries which will be returned.<br/>

# How to use locally:

Swagger landing page: https://localhost:5001/swagger/index.html <br/>

Examples of URLs how to use developed endpoint: <br/>

https://localhost:5001/api/countries?name=pol <br/>
https://localhost:5001/api/countries?name=pol&population=20 <br/>
https://localhost:5001/api/countries?name=pol&population=45 <br/>
https://localhost:5001/api/countries?name=pol&population=45&sort=descend <br/>
https://localhost:5001/api/countries?name=pol&population=45&sort=ascend <br/>

https://localhost:5001/api/countries?name=sp&population=60&sort=descend <br/>
https://localhost:5001/api/countries?name=sp&population=40&sort=descend <br/>

https://localhost:5001/api/countries?name=St&population=60&pagesize=5&sort=descend <br/>

https://localhost:5001/api/countries?name=us&population=60&pagesize=7&sort=ascend <br/>
