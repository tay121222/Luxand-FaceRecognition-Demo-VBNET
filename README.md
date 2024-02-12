# Luxand Face Recognition Demo (VB.NET)

This repository contains a VB.NET demo application showcasing the integration of Luxand API version 6.1.0 for advanced facial recognition and face detection functionalities. The application is split into two main forms:

## Face Capture Form
This form enables users to capture faces via a webcam or upload images from the file system. Upon capturing or uploading an image, the application utilizes Luxand's facial detection algorithms to identify and extract facial features.

## Face Recognition Form
In this form, users can upload an image to find potential matches within the database. The application queries the MSSQL backend, performing facial recognition using Luxand's API. It returns the matching image along with the percentage match.

## Database Setup

The application's backend utilizes MSSQL with the following table structure:

```sql
CREATE TABLE [dbo].[image_data](
    [id] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
    [MyID] [nvarchar](50) NULL,
    [Image] [image] NULL,
    [FacePositionXc] [int] NULL,
    [FacePositionYc] [int] NULL,
    [FacePositionW] [int] NULL,
    [FacePositionAngle] [float] NULL,
    [Eye1X] [int] NULL,
    [Eye1Y] [int] NULL,
    [Eye2X] [int] NULL,
    [Eye2Y] [int] NULL,
    [Template] [image] NULL,
    [FaceImage] [image] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
```
## Screenshots

**Face Capture Form**

![Face Capture Form](https://github.com/tay121222/Luxand-FaceRecognition-Demo-VBNET/assets/6275272/3b766c71-ef39-4ddd-b875-2d473fe4c0b8)


**Face Recognition Form**

![face recognition form](https://github.com/tay121222/Luxand-FaceRecognition-Demo-VBNET/assets/6275272/1d670a75-f6ac-4ab4-ae9b-c27659d082f7)

## Getting Started

To get started with this demo application, follow these steps:

1. Clone this repository to your local machine.
2. Open the solution file (`LuxandFaceRecognitionDemo.sln`) in Visual Studio.
3. Ensure you have the necessary dependencies, including Luxand API version 6.1.0.
4. Set up the MSSQL database with the provided schema.
5. Build and run the application.

## Usage

1. **Face Capture:** Use this form to capture faces via a webcam or upload images from the file system.
2. **Face Recognition:** Upload an image to find potential matches within the database. The application will return the matching image along with the percentage match.

## Contributing

Contributions are welcome! If you'd like to contribute to this project, please fork the repository and submit a pull request with your changes.

## License

This project is licensed under the [MIT License](LICENSE).

