import { useEffect, useState } from 'react';
import AddEditUser from './add-edit-user.component'
import { useParams } from 'react-router-dom';
import { UserModel } from '../models/user.model';
import axios from 'axios';

export default function UserComponent() {
  const { id } = useParams();
  const [data, setData] = useState<UserModel>({
    designation: '',
    email: '',
    employeeType: 0,
    firstName: '',
    id: 0,
    lastName: '',
    mobileNo: '',
    nationality: '',
    passportExpirtDate: '',
    passportNo: '',
    employeeTypeName: ''
  });

  useEffect(() => {
    if (id) {
      // Populate formData with initialData for Edit mode
      axios.get<UserModel>(`https://localhost:44367/api/users/${id}`)
      .then(response => {
          const data = response.data;
          setData(data);         
      })
      .catch(error => {
        console.error('Error fetching data:', error);
      });
    }
    else
    {
      //setData(null);
    }
  }, []);

  const handleDownload = async () => {
    const apiUrl = `https://localhost:44367/api/users/${id}/DownloadFile`;

    try {
      const response = await fetch(apiUrl);
      
      if (response.ok) {
        const blob = await response.blob();
        const url = window.URL.createObjectURL(blob);
        const a = document.createElement("a");
        a.href = url;
        a.download = "filename.pdf"; // Specify the file name
        document.body.appendChild(a);
        a.click();
        a.remove();
      } else {
        console.error("Download failed");
      }
    } catch (error) {
      console.error("An error occurred:", error);
    }
  };

  return (
    <div>
      <AddEditUser isEditMode={id != null} data={data} onDownload={handleDownload}/>
    </div>
  )
}
