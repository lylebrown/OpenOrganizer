# Categories
## GET Requests
GET `.../api/v1/categories/`
Returns a list of categories (note the `[]` brackets), along with their parents in expanded form:
```
[
    {
        "id": 1,
        "name": "Cat1 Without Parent",
        "parent": null
    },
    {
        "id": 2,
        "name": "Cat2 With Parent",
        "parent": {
            "id": 1,
            "name": "Cat1 Without Parent",
            "parent": null
        }
    }
 ]
 ```
 
 GET `.../api/v1/categories/{id}`
 Returns a single category by id, along with parents in expanded form:
 ```
 {
    "id": 2,
    "name": "Cat2 With Parent",
    "parent": {
        "id": 1,
        "name": "Cat1 Without Parent",
        "parent": null
    }
}
```
## POST Request
POST `../api/v1/categories/`
With a body of:
```
{
    "name": "2020 Cat2 With Parent"
}
```
And an optional header with a Key of `Parent` and a Value of `{id}`
Returns the resulting Category id as an int.

## PUT Request
PUT `../api/v1/categories/{id}`
With a body of
```
{
    "name": "2020 Cat2 With Parent EDIT"
}
```
And an optional header with a Key `Parent` and a Value of `{id}`
Returns the Category id as an int.

## DELETE Request
DELETE `../api/v1/categories/{id}`
Returns an empty 200 OK response.

# Locations
Need to document
# Tags
Need to document
# ItemAttachments
Need to document
# ItemFields
Need to document
# ItemFieldValues
Need to document
# Items
Need to document
# ItemTags
Need to document
