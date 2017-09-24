package hu.bme.aut.workoutplaner.fragment;

import android.app.AlertDialog;
import android.app.Dialog;
import android.content.DialogInterface;
import android.content.Intent;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.net.Uri;
import android.os.Bundle;
import android.os.Environment;
import android.provider.MediaStore;
import android.support.annotation.NonNull;
import android.support.v4.content.FileProvider;
import android.support.v7.app.AppCompatDialogFragment;
import android.view.LayoutInflater;
import android.view.View;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.EditText;
import android.widget.ImageView;
import android.widget.Spinner;

import java.io.File;
import java.io.IOException;
import java.text.SimpleDateFormat;
import java.util.Date;

import hu.bme.aut.workoutplaner.R;
import hu.bme.aut.workoutplaner.model.Exercise;

/**
 * Created by Balazs on 2017. 04. 21..
 */

public class AddExerciseDialogFragment extends AppCompatDialogFragment {
    static final int REQUEST_IMAGE_CAPTURE = 1;
    private String mCurrentPhotoPath;
    private EditText nameEt;
    private Spinner msSpinner;
    private EditText descEt;
    private Button addImageBtn;
    private ImageView myImageView;
    private String ms;
    private View myView;
    private  ExerciseAddedInterface listener;
    public interface ExerciseAddedInterface{
        void onExerciseAdded(Exercise e);
    }

    public AddExerciseDialogFragment(ExerciseAddedInterface lis) {
        listener = lis;
    }

    @Override
    public Dialog onCreateDialog(final Bundle savedInstanceState) {
        return new AlertDialog.Builder(getContext())
                .setTitle("Gyakorlat készítése")
                .setView(initView())
                .setPositiveButton(R.string.save, new DialogInterface.OnClickListener() {
                    @Override
                    public void onClick(DialogInterface dialog, int which) {
                        String name = nameEt.getText().toString().substring(0,1).toUpperCase();


                        Exercise exercise = new Exercise(
                                name+nameEt.getText().toString().substring(1,nameEt.getText().toString().length()) ,
                                descEt.getText().toString(),
                                ms);
                        if(mCurrentPhotoPath==null)
                            exercise.save();
                        else{
                            exercise.setImagePath(mCurrentPhotoPath);
                            exercise.save();
                        }
                        listener.onExerciseAdded(exercise);
                        dismiss();
                    }
                })
                .setNegativeButton(R.string.cancel, new DialogInterface.OnClickListener() {
                    @Override
                    public void onClick(DialogInterface dialog, int which) {
                            dismiss();
                    }
                })
                .create();
    }

    @NonNull
    private View initView() {
        View rootView = LayoutInflater.from(getContext()).inflate(R.layout.fragment_add_exercise, null);
        nameEt = (EditText) rootView.findViewById(R.id.et_add_exercise_name);
        descEt = (EditText) rootView.findViewById(R.id.et_add_exercise_desc);
        msSpinner = (Spinner) rootView.findViewById(R.id.picker_ms);
        ArrayAdapter<CharSequence> adapter = ArrayAdapter.createFromResource(getContext(),
                R.array.musclegroup_array, android.R.layout.simple_spinner_item);
        adapter.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);
        msSpinner.setAdapter(adapter);
        msSpinner.setOnItemSelectedListener(new AdapterView.OnItemSelectedListener() {
            @Override
            public void onItemSelected(AdapterView<?> parent, View view, int position, long id) {
                ms = (String) parent.getItemAtPosition(position);
            }

            @Override
            public void onNothingSelected(AdapterView<?> parent) {

            }
        });
        addImageBtn = (Button) rootView.findViewById(R.id.add_image_btn);
        addImageBtn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                dispatchTakePictureIntent();
            }
        });
        myImageView = (ImageView) rootView.findViewById(R.id.iv_image);
        myView = rootView;
        return rootView;
    }

    static final int REQUEST_TAKE_PHOTO = 1;
    private void dispatchTakePictureIntent() {
        Intent takePictureIntent = new Intent(MediaStore.ACTION_IMAGE_CAPTURE);
        // Ensure that there's a camera activity to handle the intent
        if (takePictureIntent.resolveActivity(getActivity().getPackageManager()) != null) {
            // Create the File where the photo should go
            File photoFile = null;
            try {
                photoFile = createImageFile();
            } catch (IOException ex) {
              ex.printStackTrace();
            }
            // Continue only if the File was successfully created
            if (photoFile != null) {
                Uri photoURI = FileProvider.getUriForFile(getContext(),
                        "com.example.android.fileprovider",
                        photoFile);
                takePictureIntent.putExtra(MediaStore.EXTRA_OUTPUT, photoURI);
                startActivityForResult(takePictureIntent, REQUEST_TAKE_PHOTO);
            }
        }
    }
    private File createImageFile() throws IOException {
        // Create an image file name
        String timeStamp = new SimpleDateFormat("yyyyMMdd_HHmmss").format(new Date());
        String imageFileName = "JPEG_" + timeStamp + "_";
        File storageDir = getActivity().getExternalFilesDir(Environment.DIRECTORY_PICTURES);
        File image = File.createTempFile(
                imageFileName,  /* prefix */
                ".jpg",         /* suffix */
                storageDir      /* directory */
        );

        // Save a file: path for use with ACTION_VIEW intents
        mCurrentPhotoPath = image.getAbsolutePath();
        return image;
    }

    @Override
    public void onActivityResult(int requestCode, int resultCode, Intent data) {
        super.onActivityResult(requestCode, resultCode, data);
        if (requestCode == REQUEST_IMAGE_CAPTURE && resultCode== -1) {
           setPic();
        }
    }
    private void setPic() {
        // Get the dimensions of the View
        int targetW = myImageView.getWidth();
        int targetH = myImageView.getHeight();

        // Get the dimensions of the bitmap
        BitmapFactory.Options bmOptions = new BitmapFactory.Options();
        bmOptions.inJustDecodeBounds = true;
        BitmapFactory.decodeFile(mCurrentPhotoPath, bmOptions);
        int photoW = bmOptions.outWidth;
        int photoH = bmOptions.outHeight;

        // Determine how much to scale down the image
        int scaleFactor = Math.min(photoW/targetW, photoH/targetH);

        // Decode the image file into a Bitmap sized to fill the View
        bmOptions.inJustDecodeBounds = false;
        bmOptions.inSampleSize = scaleFactor;
        bmOptions.inPurgeable = true;

        Bitmap bitmap = BitmapFactory.decodeFile(mCurrentPhotoPath, bmOptions);
        myImageView.setImageBitmap(bitmap);
    }
}
