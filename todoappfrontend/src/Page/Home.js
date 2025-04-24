import React, { useState, useEffect } from "react";
import axios from "axios";
import { Modal } from "react-bootstrap"; // Bootstrap modal import

export default function Home(props) {
  const [tableData, setTableData] = useState([]);
  const [showModal, setShowModal] = useState(false);
  const [selectedRow, setSelectedRow] = useState(null);
  const [updatedName, setUpdatedName] = useState('');
  const [updatedDescription, setUpdatedDescription] = useState('');
  const [updatedStatus, setUpdatedStatus] = useState('');

  const getallWork = () => {
    axios
      .get("http://localhost:5031/Work")
      .then(function (resp) {
        console.log(resp);
        setTableData(resp.data); // Assign all data here
      })
      .catch((error) => {
        console.error("Error fetching data:", error);
        alert("Network Error: " + error.message);
      });
  };

  const handleActionClick = (row) => {
    setSelectedRow(row); // Set the selected row data
    setUpdatedName(row.name);
    setUpdatedDescription(row.description);
    setUpdatedStatus(row.status); // Set the status to the current value of the row
    setShowModal(true); // Show the modal
  };

  const handleCloseModal = () => {
    setShowModal(false); // Close the modal
  };

  const handleUpdate = () => {
    // Create the updated data object based on the modal form values
    const updatedData = {
      id: selectedRow.id,
      name: updatedName,
      description: updatedDescription,
      status: updatedStatus,
    };
  
    // Send PUT request to update the work record in the database
    axios
      .put(`http://localhost:5031/Work`, updatedData)
      .then((response) => {
        // Successfully updated, now update local state with new data
        const updatedTableData = tableData.map((item) =>
          item.id === selectedRow.id ? response.data : item
        );
        getallWork();
        setShowModal(false); // Close the modal after successful update
      })
      .catch((error) => {
        console.error("Error updating data:", error);
        alert("Error updating data: " + error.message);
      });
  };
  

  useEffect(() => {
    getallWork();
  }, []);

  return (
    <div className="d-flex justify-content-center align-items-center vh-100">
      <div className="container">
        <div className="row justify-content-center">
          <div className="col-md-8">
            <h3 className="text-center mb-4">Todo List</h3>
            <table className="table table-striped table-bordered text-center">
              <thead className="thead-dark">
                <tr>
                  <th>ID</th>
                  <th>Name</th>
                  <th>Description</th>
                  <th>Status</th>
                  <th>Action</th>
                </tr>
              </thead>
              <tbody>
                {tableData.map((user) => (
                  <tr key={user.id}>
                    <td>{user.id}</td>
                    <td>{user.name}</td>
                    <td>{user.description}</td>
                    <td>
                      {user.status === "3" ? (
                        <i className="fas fa-check-circle me-2"></i>
                      ) : user.status === "1" ? (
                        <i className="fas fa-clock text-warning"></i>
                      ) : (
                        <i className="fas fa-spinner fa-spin text-warning"></i>
                      )}
                    </td>
                    <td>
                      <button
                        className="btn btn-info"
                        onClick={() => handleActionClick(user)} // Trigger modal on button click
                      >
                        <i className="fas fa-cogs"></i> {/* Settings Icon */}
                      </button>
                    </td>
                  </tr>
                ))}
              </tbody>
            </table>
          </div>
        </div>
      </div>

      {/* Modal for the selected row */}
      <Modal show={showModal} onHide={handleCloseModal}>
        <Modal.Header closeButton>
          <Modal.Title>Update Information for {selectedRow?.name}</Modal.Title>
        </Modal.Header>
        <Modal.Body>
          <form>
            <div className="mb-3">
              <label htmlFor="name" className="form-label">
                Name
              </label>
              <input
                type="text"
                className="form-control"
                id="name"
                value={updatedName}
                onChange={(e) => setUpdatedName(e.target.value)}
              />
            </div>
            <div className="mb-3">
              <label htmlFor="description" className="form-label">
                Description
              </label>
              <textarea
                className="form-control"
                id="description"
                value={updatedDescription}
                onChange={(e) => setUpdatedDescription(e.target.value)}
              ></textarea>
            </div>
            <div className="mb-3">
              <label htmlFor="status" className="form-label">
                Status
              </label>
              <select
                className="form-control"
                id="status"
                value={updatedStatus}
                onChange={(e) => setUpdatedStatus(e.target.value)}
              >
                <option value="1">Not Started</option>
                <option value="2">In Progress</option>
                <option value="3">Completed</option>
              </select>
            </div>
          </form>
        </Modal.Body>
        <Modal.Footer>
          <button className="btn btn-secondary" onClick={handleCloseModal}>
            Close
          </button>
          <button className="btn btn-primary" onClick={handleUpdate}>
            Update
          </button>
        </Modal.Footer>
      </Modal>
    </div>
  );
}
