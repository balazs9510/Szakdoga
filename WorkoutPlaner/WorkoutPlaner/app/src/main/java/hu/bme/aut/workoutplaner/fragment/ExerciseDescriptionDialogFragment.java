package hu.bme.aut.workoutplaner.fragment;

import android.app.AlertDialog;
import android.app.Dialog;
import android.content.DialogInterface;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.os.Bundle;
import android.support.annotation.NonNull;
import android.support.annotation.Nullable;
import android.support.v4.app.DialogFragment;
import android.view.LayoutInflater;
import android.view.View;
import android.widget.ImageView;
import android.widget.TextView;

import hu.bme.aut.workoutplaner.R;
import hu.bme.aut.workoutplaner.model.Exercise;


public class ExerciseDescriptionDialogFragment extends DialogFragment {

    TextView descTv;
    ImageView myImg;

    public static ExerciseDescriptionDialogFragment newInstance(Exercise item) {
        ExerciseDescriptionDialogFragment frag = new ExerciseDescriptionDialogFragment();
        Bundle args = new Bundle();
        args.putString("name", item.getName());
        if (item.getDescription() != null)
            args.putString("desc", item.getDescription());
        else
            args.putString("desc", "");
        if (item.getImagePath() != null) {
            args.putString("img", item.getImagePath());
        } else
            args.putString("img", "");
        frag.setArguments(args);
        return frag;
    }

    @Override
    public void onCreate(@Nullable Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
    }

    @NonNull
    @Override
    public Dialog onCreateDialog(Bundle savedInstanceState) {
        return new AlertDialog.Builder(getActivity())
                .setTitle(getArguments().getString("name"))
                .setView(initView(getArguments().getString("desc"),getArguments().getString("img") ))

                .setNegativeButton(R.string.close, new DialogInterface.OnClickListener() {
                    @Override
                    public void onClick(DialogInterface dialog, int which) {
                        dismiss();
                    }
                }).create();

    }

    private View initView(String desc, String imgPath) {
        View view = LayoutInflater.from(getContext()).inflate(R.layout.fragment_exercise_description, null);
        descTv = (TextView) view.findViewById(R.id.tv_exercise_desc);
        descTv.setText(desc);
        myImg = (ImageView) view.findViewById(R.id.iv_exercise_img);
        if(!imgPath.equals(""))
            setPic(imgPath);
        return view;
    }
    private void setPic(String mCurrentPhotoPath) {
        // Get the dimensions of the View
        int targetW = (int) getResources().getDimension(R.dimen.image_view_size);
        int targetH = (int) getResources().getDimension(R.dimen.image_view_size);

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
        myImg.setImageBitmap(bitmap);
    }
}