import React, { ChangeEvent, useEffect, useState } from "react";
import {
  UserModel,
  FileDetails,
  ModalPayload,
  formatDate,
} from "../models/user.model";
import { Button, Col, Container, Form, Row, Table } from "react-bootstrap";
import { Controller, useForm } from "react-hook-form";
import axios from "axios";
import { useNavigate } from "react-router-dom";
import CustomModal from "./custom-modal.component";
import DatePicker from "react-datepicker";
import "react-datepicker/dist/react-datepicker.css";

interface FormProps {
  isEditMode: boolean;
  data: UserModel;
  onDownload: () => void;
}

type FormValue = {
  firstname: string;
  lastname: string;
  email: string;
  mobileNo: string;
  passportNo: string;
  employeeType: number;
  nationality: string;
  designation: string;
  passportExpiryDate: string;
};

const AddEditUser: React.FC<FormProps> = ({ isEditMode, data, onDownload }) => {
  const {
    formState: { errors },
    register,
    handleSubmit,
    setValue,
    control,
  } = useForm<FormValue>();

  let navigate = useNavigate();

  const [selectedImageForPreview, setSelectedImageForPreview] = useState<
    string | null
  >(null);
  const [selectedPhoto, setSelectedPhoto] = useState<File | null>(null);
  const [modalData, setModalData] = useState<ModalPayload>({
    show: false,
    isError: false,
    message: "",
  });
  const [uploadedFile, setUploadedFile] = useState<File | null>(null);
  const [uploadedFileDetails, setUploadedFileDetails] =
    useState<FileDetails | null>(null);
  
  const [empTypeOption, setEmpTypeOption] = useState<number>(0);

  const toggleShow = () => {
    if (!modalData.isError) {
      navigate("/");
    } else {
      setModalData({ message: "", isError: false, show: false });
    }
  };

  const handleImageChange = (e: ChangeEvent<HTMLInputElement>) => {
    const file = e.target.files?.[0];
    if (file) {
      setSelectedPhoto(file);

      const reader = new FileReader();
      reader.onload = () => {
        setSelectedImageForPreview(reader.result as string);
      };
      reader.readAsDataURL(file);
    } else {
      setSelectedImageForPreview(null);
      setSelectedPhoto(null);
    }
  };

  const handleFileUpload = (event: ChangeEvent<HTMLInputElement>) => {
    const uploadedFile = event.target.files?.[0];

    if (
      uploadedFile &&
      (uploadedFile.type === "application/pdf" ||
        uploadedFile.type ===
          "application/vnd.openxmlformats-officedocument.wordprocessingml.document")
    ) {
      setUploadedFile(uploadedFile);
      const details: FileDetails = {
        name: uploadedFile.name,
        size: uploadedFile.size,
        type: uploadedFile.type,
      };
      setUploadedFileDetails(details);
    }
  };

  const handleDeleteFile = () => {
    setUploadedFile(null);
    setUploadedFileDetails(null);
  };

  const onSubmit = async (d: FormValue) => {
    const formData = new FormData();

    formData.append("id", isEditMode ? String(data.id) : String(0));
    formData.append("personPhoto", selectedPhoto as Blob);
    formData.append("passportFile", uploadedFile as Blob);

    formData.append("firstName", d.firstname);
    formData.append("lastname", d.lastname);
    formData.append("email", d.email);
    formData.append("employeeType", String(empTypeOption));
    formData.append("designation", d.designation);
    formData.append("mobileNo", d.mobileNo);
    formData.append("nationality", d.nationality);
    formData.append("passportNo", d.passportNo);
    formData.append("PassportExpirtDate", formatDate(d.passportExpiryDate));

    try {
      if (!isEditMode) {
        await axios.post("https://localhost:44367/api/users", formData, {
          headers: {
            "Content-Type": "multipart/form-data",
          },
        });

        // Handle success
        setModalData({
          message: "User created successfully",
          show: true,
          isError: false,
        });
      } else {
        await axios.put("https://localhost:44367/api/users", formData, {
          headers: {
            "Content-Type": "multipart/form-data",
          },
        });

        // Handle success
        setModalData({
          message: "User updated successfully",
          show: true,
          isError: false,
        });
      }
    } catch (error: any) {
      // Handle error
      if (axios.isCancel(error)) {
        // Request canceled
        console.log("Request canceled:", error.message);
      } else {
        // Other error
        const errorMessage = error?.response?.data?.ExceptionMessage;

        setModalData({ message: errorMessage, show: true, isError: true });
      }
    }
  };

  useEffect(() => {
    if (isEditMode) {
      // Populate formData with initialData for Edit mode
      setValue("firstname", data.firstName);
      setValue("lastname", data.lastName);
      setValue("designation", data.designation);
      setValue("nationality", data.nationality);
      setValue("email", data.email);
      //setValue("employeeType", data.employeeType);
      setValue("mobileNo", data.mobileNo);
      setValue("passportNo", data.passportNo);
      setValue("passportExpiryDate", new Date(data.passportExpirtDate).toDateString());
      setUploadedFileDetails(data.fileInfo ? data.fileInfo : null);
      setEmpTypeOption(data.employeeType);

      const fetchImage = async () => {
        try {
          const response = await axios.get(
            `https://localhost:44367/api/users/${data.id}/photo`,
            {
              responseType: "arraybuffer",
            }
          );
          const base64Data = btoa(
            new Uint8Array(response.data).reduce(
              (data, byte) => data + String.fromCharCode(byte),
              ""
            )
          );
          setSelectedImageForPreview(`data:image/jpeg;base64,${base64Data}`);

          const blob = new Blob([response.data], { type: "image/jpeg" });

          // Create a File object from the Blob
          const file = new File([blob], "image.jpg", { type: "image/jpeg" });

          setSelectedPhoto(file);
        } catch (error) {
          console.error("Error fetching image:", error);
        } finally {
        }
      };

      const fetchFile = async () => {
        try {
          const response = await axios.get(
            `https://localhost:44367/api/users/${data.id}/DownloadFile`,
            {
              responseType: "arraybuffer",
            }
          );          

          const blob = new Blob([response.data], {  type: "application/pdf" });

          // Create a File object from the Blob
          const file = new File([blob], "Passport.pdf", { type: "application/pdf" });

          setUploadedFile(file);
        } catch (error) {
          console.error("Error fetching image:", error);
        } finally {
        }
      };

      data.id > 0 && fetchImage();
      data.id > 0 && fetchFile();
    }
  }, [isEditMode, data]);

  return (
    <Container fluid="md">
      <Row>
        <Col className="text-center">
          <h1>Add User</h1>
        </Col>
      </Row>
      <Form onSubmit={handleSubmit(onSubmit)}>
        <Row>
          <Col>
            <Row>
              <Form.Group as={Col}>
                <Form.Label>Firstname</Form.Label>
                <Form.Control
                  type="text"
                  placeholder="Enter Firstname"
                  {...register("firstname", {
                    required: "Please enter your Firstname",
                  })}
                  isInvalid={!!errors.firstname}
                />

                <Form.Control.Feedback type="invalid">
                  {`${errors.firstname?.message}`}
                </Form.Control.Feedback>
              </Form.Group>

              <Form.Group as={Col}>
                <Form.Label>Lastname</Form.Label>
                <Form.Control
                  type="text"
                  placeholder="Enter Lastname"
                  {...register("lastname", {
                    required: "Please enter your Lastname",
                  })}
                  isInvalid={!!errors.lastname}
                />

                <Form.Control.Feedback type="invalid">
                  {`${errors.lastname?.message}`}
                </Form.Control.Feedback>
              </Form.Group>
            </Row>
            <Form.Group as={Col}>
              <Form.Label>Email</Form.Label>
              <Form.Control
                type="text"
                placeholder="Enter Email"
                {...register("email", {
                  required: "Please enter your Email",
                  pattern: {
                    value: /^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,4}$/i,
                    message: "Invalid email address",
                  },
                })}
                isInvalid={!!errors.email}
              />

              <Form.Control.Feedback type="invalid">
                {`${errors.email?.message}`}
              </Form.Control.Feedback>
            </Form.Group>
            <Form.Group controlId="radioGroup">
              <Form.Label>Employee Type</Form.Label>
              <Controller
                name="employeeType"
                control={control}
                defaultValue={1}
                rules={{ required: "Please select an option" }}
                render={({ field, fieldState }) => (
                  <Form.Group>
                    <Form.Check
                      {...field}
                      type="radio"
                      id="option1"
                      label="Permenant"
                      value={1}
                      checked={empTypeOption == 1}
                      onChange={() => setEmpTypeOption(1) }
                      isInvalid={fieldState.invalid}
                    />
                    <Form.Check
                      {...field}
                      type="radio"
                      id="option2"
                      label="Contract"
                      value={2}
                      checked={empTypeOption == 2}
                      isInvalid={fieldState.invalid}
                      onChange={() => setEmpTypeOption(2) }
                    />
                  </Form.Group>
                )}
              />
              <Form.Control.Feedback type="invalid">
                {`${errors.employeeType?.message}`}
              </Form.Control.Feedback>
            </Form.Group>
            <Form.Group as={Col}>
              <Form.Label>Mobile Number</Form.Label>
              <Form.Control
                type="text"
                placeholder="Enter Mobile Number"
                {...register("mobileNo", {
                  required: "Please enter your Mobile Number",
                })}
                isInvalid={!!errors.mobileNo}
              />

              <Form.Control.Feedback type="invalid">
                {`${errors.mobileNo?.message}`}
              </Form.Control.Feedback>
            </Form.Group>
            <Form.Group as={Col}>
              <Form.Label>Passport Number</Form.Label>
              <Form.Control
                type="text"
                placeholder="Enter Passport Number"
                {...register("passportNo", {
                  required: "Please enter your Passport Number",
                })}
                isInvalid={!!errors.passportNo}
              />

              <Form.Control.Feedback type="invalid">
                {`${errors.passportNo?.message}`}
              </Form.Control.Feedback>
            </Form.Group>
            <Form.Group as={Col}>
              <Form.Label>Nationality</Form.Label>
              <Form.Select
                {...register("nationality", {
                  required: "Please select nationality",
                })}
                isInvalid={!!errors.nationality}
              >
                <option value="">--- Select Nationality ----</option>
                <option value="India">India</option>
                <option value="United Arab Emirates">
                  United Arab Emirates
                </option>
                <option value="United Kingdom">United Kingdom</option>
                <option value="United Kingdom">United States</option>
              </Form.Select>

              <Form.Control.Feedback type="invalid">
                {`${errors.nationality?.message}`}
              </Form.Control.Feedback>
            </Form.Group>
            <Form.Group as={Col}>
              <Form.Label>Designation</Form.Label>
              <Form.Control
                type="text"
                placeholder="Enter Designation"
                {...register("designation", {
                  required: "Please enter your Designation",
                })}
                isInvalid={!!errors.designation}
              />

              <Form.Control.Feedback type="invalid">
                {`${errors.designation?.message}`}
              </Form.Control.Feedback>
            </Form.Group>
            <Form.Group as={Col}>
              <Form.Label>Passport Expiry Date</Form.Label>
              <Controller
                name="passportExpiryDate"
                control={control}
                render={({ field }) => (
                  <Form.Group>
                    <DatePicker
                      {...field}
                      onChange={(date) => {
                        setValue(
                          "passportExpiryDate",
                          date ? formatDate(date.toDateString()) : ""
                        );
                      }}
                      dateFormat="dd/MM/yyyy"
                    />
                  </Form.Group>
                )}
              />
            </Form.Group>

            <Form.Group>
              <Form.Label>Upload Passport</Form.Label>
              {!uploadedFileDetails ? (
                <Form.Control
                  type="file"
                  accept=".pdf"
                  onChange={handleFileUpload}
                />
              ) : (
                <Table striped bordered hover>
                  <thead>
                    <tr>
                      <th>File Name</th>
                      <th>File Size</th>
                      <th>File Type</th>
                      <th></th>
                    </tr>
                  </thead>
                  <tbody>
                    {uploadedFileDetails && (
                      <tr>
                        <td>{uploadedFileDetails.name}</td>
                        <td>{uploadedFileDetails.size} bytes</td>
                        <td>{uploadedFileDetails.type}</td>
                        <td>
                          <Button onClick={handleDeleteFile}>Delete</Button>
                          {isEditMode ? (
                            <Button onClick={() => onDownload()}>
                              Download
                            </Button>
                          ) : (
                            <></>
                          )}
                        </td>
                      </tr>
                    )}
                  </tbody>
                </Table>
              )}
            </Form.Group>
            <Button className="mt-3" variant="primary" type="submit">
              Submit
            </Button>
          </Col>
          <Col xs={6} md={4}>
            {selectedImageForPreview ? (
              <img
                src={selectedImageForPreview}
                alt="Preview"
                style={{ maxWidth: "30%", marginTop: "10px" }}
              />
            ) : (
              <div
                style={{
                  maxWidth: "30%",
                  marginTop: "10px",
                  border: "1px solid #ccc",
                  padding: "10px",
                  textAlign: "center",
                  height: "20%",
                }}
              >
                No image available
              </div>
            )}

            <input
              type="file"
              name="image"
              accept="image/*"
              onChange={handleImageChange}
            />
          </Col>
        </Row>
      </Form>
      <CustomModal
        show={modalData.show}
        handleClose={toggleShow}
        isError={modalData.isError}
        message={modalData?.message}
      />
    </Container>
  );
};

export default AddEditUser;
