import {useEffect, useState} from 'react';
import Container from 'react-bootstrap/Container';
import Row from 'react-bootstrap/Row';
import Col from 'react-bootstrap/Col';
import Table from 'react-bootstrap/Table';
import { UserModel } from '../models/user.model';
import { Button, ButtonGroup, Spinner } from 'react-bootstrap';
import { useNavigate } from 'react-router-dom';
import ConfirmationModal from './confirmation-modal.component';
import axios from 'axios';


export default function UserList() {

  const [idTobeDeleted, setIdTobeDeleted] = useState(0);
  const [showConfirmation, setShowConfirmation] = useState(false);
  const [data, setData] = useState<UserModel[] | null>(null);
  let navigate = useNavigate();

  const routeChange = () =>{ 
    let path = 'add-user'; 
    navigate(path);
  }

  const handleConfirm = () => {
    // Perform the action
    setShowConfirmation(false);

    axios.delete(`https://localhost:44367/api/users/${idTobeDeleted}`)
      .then(response => {
        setIdTobeDeleted(0);
        fetchData();
      })
      .catch(error => {
        // Handle error
        setIdTobeDeleted(0);
      });
  };

  const fetchData = () => {
    // Fetch data when the component mounts
    fetch('https://localhost:44367/api/users')
      .then(response => response.json())
      .then(data => {
        setData(data)
      })
      .catch(error => console.error('Error fetching data:', error));
  };

  useEffect(() => {
    fetchData();    
  }, []);

  return (
    <Container fluid>
      <Row className="justify-content-center">
        <Col className="text-center">
          <h1>User Details</h1>
        </Col>
      </Row>
      <Row className="pb-2">
        <Col className="d-flex justify-content-end">
          <Button variant="primary" onClick={routeChange}>
            Add User
          </Button>
        </Col>
      </Row>
      <Row>
        <Col>
          {data ? (
            <Table striped bordered hover>
              <thead>
                <tr>
                  <th>Name</th>
                  <th>Email</th>
                  <th>Employee Type</th>
                  <th>MobileNo</th>
                  <th>Designation</th>
                  <th>Action</th>
                </tr>
              </thead>
              <tbody>
                {data?.map((row, index) => (
                  <tr key={index}>
                    <td>{row.firstName + " " + row.lastName}</td>
                    <td>{row.email}</td>
                    <td>{row.employeeTypeName}</td>
                    <td>{row.mobileNo}</td>
                    <td>{row.designation}</td>
                    <td>
                      <ButtonGroup aria-label="Basic example">
                        <Button variant="primary" onClick={() => navigate(`add-user/${row.id}`)} >Edit</Button>
                        <Button variant="danger" onClick={() => {setIdTobeDeleted(row.id); setShowConfirmation(true)}}>Delete</Button>
                      </ButtonGroup>
                    </td>
                  </tr>
                ))}
              </tbody>
            </Table>
          ) : (
            <Row className="justify-content-center">
              <Spinner animation="border" role="status">
                <span className="visually-hidden">Loading...</span>
              </Spinner>
            </Row>
          )}
        </Col>
      </Row>
      <ConfirmationModal
        show={showConfirmation}
        onClose={() => setShowConfirmation(false)}
        onConfirm={handleConfirm}
        message="Are you sure you want to delete user?"
      />
    </Container>
  );
}
