
# IssueTrackingWebApi

------------


A **WebApi** exposing endpoints for **CRUD** Operations With **Issues**,  
and it's nested resources.  
It was created using **.NET 6**, **EntityFrameWork**, **SQLITE DB** and implementing **authentication** with **JWT Token**.


------------


Users can:<br>
**Register**<br>
**Log In**<br>
**Create Issue**<br>
**Get Single Issue**<br>
**Get All Issues**<br>
**Update Issue**<br>
**Delete Issue**<br>
**Add Comments To Issue**<br>
**Get Issues With Comments**<br>
**Delete Comments From Issues**<br>
**Update Comments From Issues**<br>


------------


**Register Request**

**EndPoint: api/User**

Example Value | Schema
```json
{
  "firstName": "string",
  "lastName": "string",
  "userName": "string",
  "password": "string",
  "email": "user@example.com"
}
```
Responses
Code : 200<br>
Description : Success

------------
**Log In request**<br>
EndPoint: /api/User/Login

    {
      "userName": "string",
      "password": "string"
    }

<img src="https://i.postimg.cc/Mpb0V7mP/Issue-Endponints.png">  

Users can do CRUD Operations on Comments. ~~Currently Working~~

<img src="https://i.postimg.cc/fL6S6yvS/Screenshot-2022-07-21-204216.png">  

Users can read all the issues they have logged in the system.<br>  
Users can mark issues as completed and see when completed ones where completed.

The **Log In** endpoint takes a LogIn Request Object with the following properties: