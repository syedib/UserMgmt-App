import React, { ChangeEvent, useEffect, useState } from "react";
import { FileDetails } from "../models/user.model";
import { Button, Form, Table } from "react-bootstrap";

interface PdfUploadTableProps {
  onFileChange: (file: File | null, details: FileDetails | null) => void;
  isEdit: boolean;
  fileInfoForEdit: FileDetails | null;
  onDownloadFile: () => void;
}

export default function FileUploadComponent({
  onFileChange,
  isEdit, fileInfoForEdit, onDownloadFile
}: PdfUploadTableProps) {
  const [uploadedFile, setUploadedFile] = useState<File | null>(null);
  const [fileDetails, setFileDetails] = useState<FileDetails | null>(null);

  useEffect(() => {
    if (isEdit) {
      setFileDetails(fileInfoForEdit ? fileInfoForEdit : null);
    }
  }, [])

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
      setFileDetails(details);
      onFileChange(uploadedFile, details); // Notify parent component of file and details change
    }
  };

  const handleDeleteFile = () => {
    setFileDetails(null);
    setFileDetails(null);
    onFileChange(null, null);
  };

  return (
    <div>
      <Form.Label>Upload Passport</Form.Label>
      {!fileDetails ? (
        <Form.Control
          type="file"
          accept=".pdf,.docx"
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
            {fileDetails && (
              <tr>
                <td>
                  {fileDetails.name}
                </td>
                <td>{fileDetails.size} bytes</td>
                <td>{fileDetails.type}</td>
                <td>
                  <Button onClick={handleDeleteFile}>Delete</Button>
                  {isEdit ? <Button onClick={() => onDownloadFile()}>Download</Button>: <></>}
                </td>
              </tr>
            )}
          </tbody>
        </Table>
      )}
    </div>
  );
}
