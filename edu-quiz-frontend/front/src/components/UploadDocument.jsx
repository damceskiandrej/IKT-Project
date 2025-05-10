import React, {useRef, useState} from 'react';
import CustomCircleButton from "@/components/CustomCircleButton.jsx";
import {toast} from "react-toastify";

function UploadDocument({btnText, apiEndpoint, successText, showToast}) {
    const fileInputRef = useRef(null);
    const [isUploading, setIsUploading] = useState(false);

    const handleButtonClick = () => {
        fileInputRef.current.click();
    };

    const handleFileChange = async (event) => {
        const file = event.target.files[0];
        if (file) {
            console.log('Selected file:', file);
            const formData = new FormData();
            formData.append('file', file);

            let success = false;

            try {
                setIsUploading(true);

                const response = await fetch(apiEndpoint, {
                    method: 'POST',
                    body: formData,
                });

                if (!response.ok) {
                    throw new Error('File upload failed');
                }

                const result = await response.json();
                console.log('Upload Done:', result);
                success = true;
            } catch (error) {
                console.error('Error uploading file:', error);
                toast.error("Додека прикачувавте документ се појави грешка.");
            } finally {
                setIsUploading(false);
                if (success && showToast) {
                    showToast(successText || "Датотеката е успешно прикачена.");
                }
            }
        }
    }


    return (
        <div className="form-group mb-3">
            <CustomCircleButton
                btnText={isUploading ? 'Uploading...' : btnText}
                onClick={handleButtonClick}
                disabled={isUploading}
            />
            <input
                type="file"
                ref={fileInputRef}
                style={{display: 'none'}}
                onChange={handleFileChange}
            />
        </div>
    );
}

export default UploadDocument;