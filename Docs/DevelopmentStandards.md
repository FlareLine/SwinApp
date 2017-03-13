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
### Event Handling
- I don't mind where you set your event handling for your XAML but stick with it for the page at least. 
    - If you use the XAML means of using the associated attribute use that for elements on the page
        - `<Element x:Name="TestElement" Clicked="ClickEvent"/>`
    - If you prefer to set your events in the main method of the page that is fine too but do that for all elements
        - `TestElement.Clicked += ClickEvent;`
- Note that I prefer using your .cs file to set events only because it provides richer intellisense and autocompletion options than setting it in the XAML file
- Events should be explicitly defined as opposed to being anonymous arrow functions
    - Using Visual Studio's autocompletion will fix this
- Do not use shit names for your events, they need to be descriptive and also generic so that
multiple elements may use them if need be
    - Rather than do something like `ClickEvent` describe the action taking place
- Keep the event code to a minimum and instead write the bulk of the code in seperate functions that are called by the event.
    - This allows for code to be reused as events can only be mapped to similar elements

### Data Binding
- This is a must, if it is possible to data bind something then you must do it.
- Data binding is necessary as it is a sure fire way of knowing that:
    - Our elements are up to date
    - The data being presented is formatted according to the .xaml
    - Our code is clean and lean
- Learn the Model-View-ViewModel (MVVM) pattern as any solution that works with XAML is best when utilising this
    - It is also a good job skill so hey

---
## CSharp

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

### Asynchronous Programming
- Async/Await is the best feature of C# for modern application development and if you don't believe me fight me dog
- Async/Await must be used for any of the following scenarios:
    - 