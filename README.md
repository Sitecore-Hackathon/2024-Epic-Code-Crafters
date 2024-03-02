![Hackathon Logo](docs/images/hackathon.png?raw=true "Hackathon Logo")
# Sitecore Hackathon 2024

- MUST READ: **[Submission requirements](SUBMISSION_REQUIREMENTS.md)**
- [Entry form template](ENTRYFORM.md)
  
### ⟹ [Insert your documentation here](ENTRYFORM.md) <<

# AI Media Library Search

## Summary
AI Media Library Search enables users to upload images using Sitecore media library, once the user publishes the image, it utilizes AI through the Google Cloud Vision API to generate descriptive labels for the published image. This allows the user to dynamically upload images alongside useful information that can later be used for example as alternative text when inserting the image into a website, also improving accessibility and readability of the site. 

In addition to that, a custom search engine was built to provide the user with the possibility of quickly retrieving all images that match the chosen keyword with the AI-generated labels.

## Google Cloud Vision implementation

This feature leverages a **RESTful** service to interact with the Google Cloud Vision service, which analyzes the content of the image and returns a list of labels that describe the image's content. By passing the URL of the image to the Google Cloud Vision API, users can upload their images alongside relevant labels that provide insights into the visual elements present within the image. Everything is done under the hood, without the user having to worry avoid the process thanks to our Sitecore implementation.

## Functionality Overview

-  **Image Upload**: Users can upload images directly within the Sitecore Media Library interface.

-  **Google Cloud Vision Integration**: Once the user publishes the image, Sitecore will upload the image and then the url will be sent to Google Cloud Vision API to analyze the content of the uploaded image.

-  **Label Generation**: Upon image analysis, the Google Cloud Vision API generates descriptive labels that characterize the visual elements depicted in the image.

-  **Dynamic image labelling**: After Google Cloud Vision is done generating the labels, they are returned back to Sitecore to automatically add them to its corresponding image.

-  **Accessibility and Insights**: The generated labels provide valuable insights into the content of the image, enhancing accessibility and facilitating content management within the Sitecore platform.

-  **Search engine**: Users can easily locate images by typing any keyword into the search bar, this will then scan for all images and show them if they match the corresponding AI-generated label.

## Phases

```mermaid
graph TB

subgraph Search page
G[User goes to Search engine] -- Enters keyword <br/>into search bar  --> H[Frontend fetches images<br/>according to seach query]  --> I[Images are shown on the <br/>interface alongside its labels]
end

subgraph Sitecore implementation
A[User publishes image] -- Sitecore uploads it <br/>and sends the url to GCV API --> 
B[GCV service scans the image <br/>and generates all labels] -- GCV returns the labels <br/>back to sitecore
--> E[Sitecore updates the <br/>image with the labels] 
-->F[Image is shown <br/>in Media Gallery]
end
```
