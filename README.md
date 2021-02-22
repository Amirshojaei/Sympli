# Simple search engine crawler for Sympli

#### Techs used in this project: .net core, reactjs(hooks), moq and xunit for unit tests

#### notation convention: for back-end parameters, private members and local variables are camel case. structures(class, interface,...), public members are pascal case
#### for front-end everything is in camel case except structures like class, components, and ...

## Some notes
By Implementing ISearchEngine interface you can add as many as search engines you like to the app. Google and Bing are added as a sample. and the user can choose.
since the test mentioned not to use the rd party framework I could use moles to mock extensions method for cache, so just instantiated it manually.
