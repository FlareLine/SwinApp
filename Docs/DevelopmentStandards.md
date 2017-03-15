# Development Standards and Conventions Document
> Please follow these standards so that our code doesn't look bad and so development time is greatly increased.

Current Contributors (blame these people if they suck):

- Alex Billson

---

## XAML
### Element Naming Conventions
- Elements in a XAML page should be identified so that at least part of the element's name is present before the identifier
    - `x:Name="{ElementName}{Identifier}"`
- Multiple word element names (such as StackLayout) can be shortened to one for ease of typing
    - There are instances where this will overlap and you must use common sense to name stuff
- Eg:
```xml
    <!--StackLayout == Stack-->
    <StackLayout x:Name="StackMain">
        <Entry x:Name="EntryName" Placeholder="Name"/>
        <Button x:Name="ButtonConfirm" Content="Confirm"/>
    </StackLayout>
```
### Element Structure
- If the element is a container, use typical XML brackets
- For everything else, the tag must be self closing
    - Whilst this isn't exactly well formed XAML's design is based around C# classes and properties and so each attribute of the XAML element is linked to the class' property
    - There are a few times where using it like a HTML tag may work, this doesn't necessarily mean you should use it
        - Don't: `<Label>Hello</Label>`
        - Do: `<Label Text="Hello"/>` 
### Event Handling
- I don't mind where you set your event handling for your XAML but stick with it for the page at least. 
    - If you use the XAML means of using the associated attribute use that for elements on the page
        - `<Element x:Name="TestElement" Clicked="ClickEvent"/>`
        - `TestElement.Clicked += ClickEvent;`
- Events should be explicitly defined as opposed to being anonymous arrow functions
- Do not use shit names for your events, they need to be descriptive and also generic so that
multiple elements may use them if need be
- Keep the event code to a minimum and instead write the bulk of the code in seperate functions that are called by the event.
    - This allows for code to be reused as events can only be mapped to similar elements
### Data Binding
- Data binding is necessary as it is a sure fire way of knowing that:
    - Our elements are up to date
    - The data being presented is formatted according to the .xaml
    - Our code is clean and lean
- Learn the Model-View-ViewModel (MVVM) pattern as any solution that works with XAML is best when utilising this
    - It is also a good job skill so hey

---
## C\#

### Language Conventions
- Any formatting related to code indentation and presentation can usually be fixed with a linter or built in key shortcuts
    - Visual Studio: `Ctrl + K + D`
    - Visual Studio Code: `Alt + Shift + f`
- Private members are camelcased and proceed with an underscore
    - `private string _privateString = "Shhhhh";`
- Public members use capitalization with no underscores in between words
    - `public string PublicString = "YO";`
- Indentation should use tabs
- Curly braces should be placed on the line after the definition, not on the same line
- Class names must be capitalized
- Async code must end the in the suffix `Async`
- Interfaces must begin with the prefix of a capital I
### Asynchronous Programming
- Async/Await is the best feature of C# for modern application development and if you don't believe me fight me dog
- Async/Await must be used for any of the following scenarios:
    - Downloading Content from the Internet
    - Navigating between pages
    - Application functionality (where available)
    - Any loading of large amounts of data
        - Times where databinding is not an option yet the data is large
- The reason we must utilize asynchronous programming is purely for UX:
    - Reduces lag
    - Allows for loading screens
    - Gives the user freedom to do things whilst another thing loads
- Async in C# has a modern set of conventions which can be found [here](https://gist.github.com/jonlabelle/841146854b23b305b50fa5542f84b20c)

---
## Git

### Usage

- Do not use Git sparingly by any means, if you make a change; commit the code.
- Having smaller commits is much more beneficial than larger commits as it means that the commits are more meaningful and signify the true steps taken to develop a solution
- Use a text editor/IDE with built in Git support and also ensure it has a "Sync" function as that helps to avoid merge conflicts
    - Sync functionality in Git basically pulls and pushes in one action so that your code is kept up to date with each change you commit
    - Visual Studio and VS Code have this built in but I'm not sure about other editors
- Avoid editing the master branch directly
- Make sure your commits have appropriate summaries and/or descriptions
- The only repercussion to committing too much is that fucking bot mighn't shut up
**Let's make a ripper app gaiz**
