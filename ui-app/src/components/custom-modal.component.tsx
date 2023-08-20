import React from 'react';
import Modal from 'react-bootstrap/Modal';
import Button from 'react-bootstrap/Button';

interface MyModalProps {
  show: boolean;
  message?: string;
  isError: boolean;
  handleClose: () => void;
}

const CustomModal: React.FC<MyModalProps> = ({ show, handleClose, message, isError }) => {
  return (
    <Modal show={show} onHide={handleClose}>
        <Modal.Header closeButton>
          <Modal.Title>{isError ? 'Error' : 'Message'}</Modal.Title>
        </Modal.Header>
        <Modal.Body>
          <p>{message}</p>
        </Modal.Body>
        <Modal.Footer>
          <Button variant="secondary" onClick={handleClose}>
            Close
          </Button>
        </Modal.Footer>
      </Modal>
  );
};

export default CustomModal;
