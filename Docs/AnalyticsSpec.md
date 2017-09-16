# SwinApp Analytics Specification

Alex Billson

## Foreword

Whilst not necessarily implemented in its entirety on both a client and server
side, SwinApp comes with telemtry functionality for recording various analytics
in relation to how the user interacts with the application.

This document will exaplin the structure of the analytics data as well as how
an endpoint could be configured to receive this data if such functionality was desired in future.

## The Workflow of SwinApp's Analytics

All analytics functionality is in the suitably named  `Analytics.cs` file 
(located in the `Library` folder). 

The workflow of `Analytics` is very straightforward, the entire process working
around logging `AppEvent`'s. An `AppEvent` (located in the same folder as 
`Analytics`) is a relatively passive data structure which can be comparable to 
Android's `Intent` but rather than acting as a means of working with the device's
functionality, it aims to take note of actions that occur in the application.

