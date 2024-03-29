![Hackathon Logo](docs/images/hackathon.png?raw=true "Hackathon Logo")
# Sitecore Hackathon 2024

# AI Media Library Search

## Team name
⟹ Epic Code Crafters

## Category
⟹ Best use of AI

## Description
AI Media Library Search allows users to upload images using the Sitecore media library, and once the user publishes the image, it utilizes AI through the Google Cloud Vision API to generate descriptive tags for the published image. This enables users to dynamically upload images along with useful information that can be used later, for example, as alternative text when inserting the image on a website, thereby improving accessibility and readability of the site. Additionally, a custom search engine was created to offer users the possibility of quickly retrieving all images that match the chosen keyword with the tags generated by AI.

Google Cloud Vision
Google Cloud Vision AI is a powerful artificial intelligence tool that allows extracting valuable information from images and videos through pre-trained APIs, AutoML, or custom models. That's why we have selected it to help us achieve our goal of making life easier for content editors when searching for images within the extensive media library.

As a next step, content editors will have access to a API, that will allow them to search within the media library, only by entering the keyword or keywords of what they need in the image. The search process will be executed, resulting in a list with all the images that meet the given conditions. This way they can access the GUID of the image they are looking for more quickly.

Function Summary

Image uploading: Users can upload images directly in the Sitecore Media Library interface.
Integration with Google Cloud Vision: Once the user publishes the image, Sitecore will load the image, and then the item will go through the conversion process, to then be sent to the Google Cloud Vision API to analyze the content of the uploaded image.
Tag generation: After analyzing the image, the Google Cloud Vision API generates descriptive tags characterizing the visual elements represented in the image.
Dynamic image tagging: Once Google Cloud Vision has finished generating the tags, they are returned to Sitecore to be automatically added to their corresponding image.
Accessibility and information: The generated tags provide valuable information about the content of the image, improving accessibility and facilitating content management within the Sitecore platform.
Search engine: Users can easily locate images by typing any keyword in the search bar, which will search for all images and display them if they match the corresponding tag generated by AI.

## Google Cloud Vision implementation

This feature leverages a **RESTful** service to interact with the Google Cloud Vision service, which analyzes the content of the image and returns a list of labels that describe the image's content. By passing the URL of the image to the Google Cloud Vision API, users can upload their images alongside relevant labels that provide insights into the visual elements present within the image. Everything is done under the hood, without the user having to worry avoid the process thanks to our Sitecore implementation.

## Functionality Overview

-  **Google Cloud Vision Integration**: Once the user rebuild the index in Sitecore, each item of the media library will connect to Google Cloud Vision API to generate a set of tags that describes each image.

-  **Label Generation**: Upon image analysis, the Google Cloud Vision API generates descriptive labels that characterize the visual elements depicted in the image.

-  **Solr Indexing**: Every tag is stored in Solr for searching purposes.

  ![image](https://github.com/Sitecore-Hackathon/2024-Epic-Code-Crafters/assets/106611133/552fd5ff-5376-48f7-bd80-13d32f474710)


-  **Dynamic image labelling**: After Google Cloud Vision is done generating the labels, they are returned back to Sitecore to automatically add them to its corresponding image.

-  **Accessibility and Insights**: The generated labels provide valuable insights into the content of the image, enhancing accessibility and facilitating content management within the Sitecore platform.

-  **Search engine**: Users can easily locate images by typing any keyword into the search bar, this will then scan for all images and show them if they match the corresponding AI-generated label.

## Video Link

https://youtu.be/i13qb_8-xyI 

## Pre-requisites and Dependencies

    • Create a Google Cloud account.
    • Create a new project in the Google Cloud Platform. Within this project, proceed to perform the following actions:
        ◦ Enable the Cloud Vision API.
        ◦ Create a Service Account, which must have a role that allows access to the use of the API. In this case, we assigned the Cloud Vision AI Service Agent role.
        ◦ Once the user is created, proceed to generate the JSON file that we will use for the connection.
        ◦ After downloading the JSON file, use the command gcloud auth activate-service-account --key-file=[full path of the JSON file] to connect to Google Cloud.
        ◦ Then, use the command gcloud auth print-access-token to generate a token that we will finally use to connect to the API.
        ◦ Another item we created was a key=API_KEY, as we made a REST connection to consume the service.
        ◦ Place the token and the API_KEY in this file: ...\2024-Epic-Code-Crafters\src\Feature\ImageFinder\Code\App_Config\Include\ECCHackaton24.Feature.Imagefinder\Feature.ImageFinder.config.

## Installation instructions

1. Download the code
2. Publish the code in a sitecore instance
3. Setup the computed field “imageDescriptionAI” into the main solr.config file.
4. Make sure Solr is up and running
5. Open the CM Launchpad
6. Open the Control Panel
7. Populate Solr managed schema
8. Rebuild the web index
9. You can start searching for tag images in the following URL:

	https://{your domain}/ImageFinder/SearchMediaLibraryImage?searchKeywords=sky 



## Usage instructions

1. Setup a GET request
2. Enter the endpoint
3. Add a searchKeywords param
4. Click send

   ![image](https://github.com/Sitecore-Hackathon/2024-Epic-Code-Crafters/assets/106611133/90447e17-f0bb-4f7a-bf60-26120c952eac)


You can use the search Enpoint from any client like Postman or directly from the Browser
