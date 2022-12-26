# To-Do List

Is a web application for maintaining to-do lists (like [Google Keep](https://keep.google.com/)) and it is a part of Novalite student practice program. The application consists of 3 segments:
* [API](#the-api)
* [Web Client](#web-client)
* [Security & Deployment](#security-&-deployment)

## The API

An REST API solution which exposes CRUD & search endpoints. Reminder functionality 
that will run as separate service. 

The API should support the following:

* Preview of to-do-list.
* Update of to-do list, title & list items (including list/item reordering).
* Creation of to-do list containing list title & list items.
* Removal of to-do list.
* Searching the to-do lists by title (with partial match & case insensitive, e.g. if search criteria is "Dark", the result should contain item with name "darko").
* Logging functionality.
* API swagger documentation.
* (optionally) Reminder functionality implying email sending for all of to-do-lists which remindMe date has expired.
* (optionally) Unit tests.

The API will include the following technologies/service providers:
* .Net Core (Web API)
* Entity Framework Core
 
## Web Client

The API will be used by the client web application. The client application will 
provide user interface which should support the 
following:

* Dashboard page containing the list/grid of all available to-do lists with search input. "Reminded" to-do lists should be on top and with proper indicator.
* Page/popup for creating/editing of to-do list/items. To-do list/item should be created/update on focus lost event (like in Google Keep).
* Removal of to-do lists/items.
* To-do lists/items position change via drag-and-drop.
* "RemindMe" logic input fields (add/remove and validations).
 
The client will include the following technologies:
* Angular 7
